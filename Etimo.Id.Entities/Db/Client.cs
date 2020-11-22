using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etimo.Id.Entities
{
    public class Client
    {
        [Key]
        public Guid ClientId { get; set; } = Guid.NewGuid();
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}
