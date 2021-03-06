// ReSharper disable InconsistentNaming

using Etimo.Id.Attributes;
using Etimo.Id.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Etimo.Id.Dtos
{
    public class RoleRequestDto
    {
        [Required]
        [MaxLength(32)]
        [NqChar]
        public string name { get; set; }

        [Required]
        [MaxLength(128)]
        [NqsChar]
        public string description { get; set; }

        [Required]
        public int? application_id { get; set; }

        public Role ToRole(Guid? roleId = null)
        {
            var role = new Role
            {
                Name          = name,
                Description   = description,
                ApplicationId = application_id ?? default,
            };

            if (roleId != null) { role.RoleId = roleId.Value; }

            return role;
        }
    }
}
