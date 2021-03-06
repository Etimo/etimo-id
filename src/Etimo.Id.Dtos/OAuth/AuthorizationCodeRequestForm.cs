// ReSharper disable InconsistentNaming

using Etimo.Id.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Etimo.Id.Dtos
{
    public class AuthorizationCodeRequestForm
    {
        [Required]
        [Unicode]
        public string username { get; set; }

        [Required]
        [Unicode]
        public string password { get; set; }
    }
}
