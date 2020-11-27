using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Etimo.Id.Entities.Abstractions;
using Etimo.Id.Service.Exceptions;

namespace Etimo.Id.Service.TokenGenerators
{
    public class ClientCredentialsTokenGenerator : IClientCredentialsTokenGenerator
    {
        private readonly IApplicationsService _applicationsService;
        private readonly IJwtTokenFactory _jwtTokenFactory;

        public ClientCredentialsTokenGenerator(
            IApplicationsService applicationsService,
            IJwtTokenFactory jwtTokenFactory)
        {
            _applicationsService = applicationsService;
            _jwtTokenFactory = jwtTokenFactory;
        }

        public async Task<JwtToken> GenerateTokenAsync(IClientCredentialsRequest request)
        {
            ValidateRequest(request);

            var application = await _applicationsService.AuthenticateAsync(new Guid(request.ClientId), request.ClientSecret);

            var jwtRequest = new JwtTokenRequest
            {
                Audience = new List<string> { application.HomepageUri },
                Subject = application.ClientId.ToString()
            };

            var jwtToken = _jwtTokenFactory.CreateJwtToken(jwtRequest);

            return jwtToken;
        }

        private static void ValidateRequest(IClientCredentialsRequest request)
        {
            if (request.ClientId == null || request.ClientSecret == null)
            {
                throw new InvalidClientException("Invalid client credentials.");
            }
        }
    }
}