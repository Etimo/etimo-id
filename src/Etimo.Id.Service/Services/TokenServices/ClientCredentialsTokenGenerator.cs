using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using Etimo.Id.Entities.Abstractions;
using Etimo.Id.Service.Constants;
using Etimo.Id.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etimo.Id.Service.TokenGenerators
{
    public class ClientCredentialsTokenGenerator : IClientCredentialsTokenGenerator
    {
        private readonly IAccessTokenRepository     _accessTokenRepository;
        private readonly IAuthenticateClientService _authenticateClientService;
        private readonly IJwtTokenFactory           _jwtTokenFactory;
        private readonly IRequestContext            _requestContext;
        private          Application                _application;

        private IClientCredentialsTokenRequest _request;

        public ClientCredentialsTokenGenerator(
            IAuthenticateClientService applicationService,
            IAccessTokenRepository accessTokenRepository,
            IJwtTokenFactory jwtTokenFactory,
            IRequestContext requestContext)
        {
            _authenticateClientService = applicationService;
            _accessTokenRepository     = accessTokenRepository;
            _jwtTokenFactory           = jwtTokenFactory;
            _requestContext            = requestContext;
        }

        public async Task<JwtToken> GenerateTokenAsync(IClientCredentialsTokenRequest request)
        {
            _request = request;

            UpdateContext();
            await ValidateRequestAsync();
            JwtToken jwtToken    = await CreateJwtTokenAsync();
            var      accessToken = jwtToken.ToAccessToken();
            _accessTokenRepository.Add(accessToken);
            await _accessTokenRepository.SaveAsync();

            return jwtToken;
        }

        private void UpdateContext()
            => _requestContext.ClientId = _request.ClientId;

        private async Task ValidateRequestAsync()
        {
            if (_request.ClientId == Guid.Empty || _request.ClientSecret == null)
            {
                throw new InvalidClientException("Invalid client credentials.");
            }

            _application = await _authenticateClientService.AuthenticateAsync(_request.ClientId, _request.ClientSecret);
            if (_application.Type == ClientTypes.Public)
            {
                throw new UnauthorizedClientException("Public clients cannot use the client credentials grant.");
            }
        }

        private Task<JwtToken> CreateJwtTokenAsync()
        {
            var jwtRequest = new JwtTokenRequest
            {
                Audience        = new List<string> { _request.ClientId.ToString() },
                Subject         = _application.UserId.ToString(),
                LifetimeMinutes = _application.AccessTokenLifetimeMinutes,
            };

            return _jwtTokenFactory.CreateJwtTokenAsync(jwtRequest);
        }
    }
}
