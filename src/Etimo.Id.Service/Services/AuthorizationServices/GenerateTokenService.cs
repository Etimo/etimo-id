using Etimo.Id.Abstractions;
using Etimo.Id.Constants;
using Etimo.Id.Entities;
using Etimo.Id.Entities.Abstractions;
using Etimo.Id.Exceptions;
using System.Threading.Tasks;

namespace Etimo.Id.Service
{
    public class GenerateTokenService : IGenerateTokenService
    {
        private readonly IAuthorizationCodeTokenGenerator        _authorizationCodeTokenGenerator;
        private readonly IClientCredentialsTokenGenerator        _clientCredentialsTokenGenerator;
        private readonly IImplicitTokenGenerator                 _implicitTokenGenerator;
        private readonly IRefreshTokenGenerator                  _refreshTokenGenerator;
        private readonly IResourceOwnerCredentialsTokenGenerator _resourceOwnerCredentialsTokenGenerator;

        public GenerateTokenService(
            IAuthorizationCodeTokenGenerator authorizationCodeTokenGenerator,
            IClientCredentialsTokenGenerator clientCredentialsTokenGenerator,
            IImplicitTokenGenerator implicitTokenGenerator,
            IResourceOwnerCredentialsTokenGenerator resourceOwnerCredentialsTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator)
        {
            _authorizationCodeTokenGenerator        = authorizationCodeTokenGenerator;
            _clientCredentialsTokenGenerator        = clientCredentialsTokenGenerator;
            _implicitTokenGenerator                 = implicitTokenGenerator;
            _resourceOwnerCredentialsTokenGenerator = resourceOwnerCredentialsTokenGenerator;
            _refreshTokenGenerator                  = refreshTokenGenerator;
        }

        public Task<JwtToken> GenerateTokenAsync(ITokenRequest request)
            => request.GrantType switch
            {
                GrantTypes.AuthorizationCode => _authorizationCodeTokenGenerator.GenerateTokenAsync(request),
                GrantTypes.ClientCredentials => _clientCredentialsTokenGenerator.GenerateTokenAsync(request),
                GrantTypes.Implicit          => _implicitTokenGenerator.GenerateTokenAsync(request),
                GrantTypes.Password          => _resourceOwnerCredentialsTokenGenerator.GenerateTokenAsync(request),
                GrantTypes.RefreshToken      => _refreshTokenGenerator.GenerateTokenAsync(request),
                _                            => throw new UnsupportedGrantTypeException("Grant type not supported."),
            };
    }
}
