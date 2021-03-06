using Etimo.Id.Abstractions;
using Etimo.Id.Constants;
using Etimo.Id.Entities;
using Etimo.Id.Entities.Abstractions;
using Etimo.Id.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Etimo.Id.Service.TokenGenerators
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly IGetRolesService _getRolesService;
        private readonly JwtSettings      _settings;

        public JwtTokenFactory(IGetRolesService getRolesService, JwtSettings settings)
        {
            _getRolesService = getRolesService;
            _settings        = settings;
        }

        public async Task<JwtToken> CreateJwtTokenAsync(IJwtTokenRequest request)
        {
            string   audiences = CompileAudiences(request.Audience);
            DateTime expiresAt = DateTime.UtcNow.AddMinutes(request.LifetimeMinutes);
            var      tokenId   = Guid.NewGuid();

            var claims = new List<Claim>
            {
                // https://tools.ietf.org/html/rfc7519#section-4.1
                new(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new(JwtRegisteredClaimNames.Sub, request.Subject),
                new(JwtRegisteredClaimNames.Aud, audiences, JsonClaimValueTypes.JsonArray),
                new(JwtRegisteredClaimNames.Exp, GetUnixTime(expiresAt).ToString(), ClaimValueTypes.Integer32),
                new(JwtRegisteredClaimNames.Nbf, GetUnixTime(DateTime.UtcNow.AddMinutes(-5)).ToString(), ClaimValueTypes.Integer32),
                new(JwtRegisteredClaimNames.Iat, GetUnixTime(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer32),
                new(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
                new(JwtRegisteredClaimNames.Azp, request.ClientId.ToString()),

                // https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
                new(OpenIdConnectClaimTypes.PreferredUsername, request.Username),
            };

            // https://tools.ietf.org/html/rfc7519#section-4.2
            List<Role> roles = await _getRolesService.GetByUserIdAsync(new Guid(request.Subject));
            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Name)));

            // https://tools.ietf.org/html/rfc8693#section-4.2
            string scopes;
            if (request.Scope != null) { scopes = request.Scope; }
            else
            {
                var                 roleScopes   = roles.SelectMany(r => r.Scopes).ToList();
                IEnumerable<string> uniqueScopes = roleScopes.Select(s => s.Name).Distinct();
                scopes = string.Join(" ", uniqueScopes);
            }

            claims.Add(new Claim(CustomClaimTypes.Scope, scopes));

            SigningCredentials signingCredentials;
            if (_settings.PrivateKey != null && _settings.PublicKey != null)
            {
                var rsa = RSA.Create(2048);
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(_settings.PrivateKey), out int _);
                signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
            }
            else if (_settings.Secret != null)
            {
                byte[] secretBytes = Encoding.UTF8.GetBytes(_settings.Secret);
                var    key         = new SymmetricSecurityKey(secretBytes);
                signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            }
            else { throw new Exception("Could not sign token because both symmetric secret and asymmetric keys are missing."); }


            var    token     = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);
            string tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtToken
            {
                TokenId     = tokenId,
                AccessToken = tokenJson,
                TokenType   = TokenTypes.Bearer,
                ExpiresIn   = GetSecondsUntil(expiresAt),
                ExpiresAt   = expiresAt,
                Scope       = scopes,
            };
        }

        private string CompileAudiences(List<string> audiences)
        {
            // We need to add this api as audience
            if (!audiences.Contains(_settings.Issuer)) { audiences.Add(_settings.Issuer); }

            IEnumerable<string> nonEmptyAudiences = audiences.Where(a => !string.IsNullOrWhiteSpace(a));
            IEnumerable<string> trimmedAudiences  = nonEmptyAudiences.Select(a => a.Trim());
            return JsonSerializer.Serialize(trimmedAudiences);
        }

        private static int GetSecondsUntil(DateTime dateTime)
            => GetSecondsBetween(DateTime.UtcNow, dateTime);

        private static int GetUnixTime(DateTime dateTime)
            => GetSecondsBetween(new DateTime(1970, 1, 1), dateTime);

        private static int GetSecondsBetween(DateTime from, DateTime until)
            => Math.Abs((int)Math.Ceiling((until - from).TotalSeconds));
    }
}
