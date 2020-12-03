using System;

namespace Etimo.Id.Entities
{
    public class AccessToken
    {
        public Guid AccessTokenId { get; set; }
        public bool Disabled { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        public bool IsExpired => ExpirationDate > DateTime.UtcNow;
    }
}
