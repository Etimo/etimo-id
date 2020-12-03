using System;
using System.Collections.Generic;

namespace Etimo.Id.Entities
{
    public class JwtToken
    {
        public Guid TokenId { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Scopes { get; set; }

        public AccessToken ToAccessToken()
        {
            return new AccessToken
            {
                AccessTokenId = TokenId,
                ExpirationDate = ExpiresAt
            };
        }
    }
}
