// ReSharper disable InconsistentNaming

using Etimo.Id.Api.Attributes;
using Etimo.Id.Entities;
using Etimo.Id.Service.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Etimo.Id.Api.OAuth
{
    public class AccessTokenRequestForm
    {
        [Required]
        [ValidValues(
            "password",
            "client_credentials",
            "authorization_code",
            "refresh_token")]
        public string grant_type { get; set; }

        public Guid? client_id { get; set; }

        [VsChar]
        public string client_secret { get; set; }

        [ValidUri]
        public string redirect_uri { get; set; }

        [VsChar]
        public string refresh_token { get; set; }

        [Unicode]
        public string username { get; set; }

        [Unicode]
        public string password { get; set; }

        [VsChar]
        public string code { get; set; }

        [NqsChar] // Should be NQCHAR but since we're splitting these values with a space, we need to allow space as well.
        public string scope { get; set; }

        public TokenRequest ToTokenRequest()
        {
            TokenRequest tokenRequest = grant_type switch
            {
                "password"           => new ResourceOwnerPasswordCredentialsTokenRequest(username, password, scope),
                "client_credentials" => new ClientCredentialsTokenRequest(scope),
                "authorization_code" => new AuthorizationCodeTokenRequest(code, redirect_uri, client_id ?? Guid.Empty),
                "refresh_token"      => new RefreshTokenRequest(refresh_token, scope),
                _                    => throw new UnsupportedGrantTypeException("Invalid grant type."),
            };

            tokenRequest.ClientSecret = client_secret;

            return tokenRequest;
        }
    }
}
