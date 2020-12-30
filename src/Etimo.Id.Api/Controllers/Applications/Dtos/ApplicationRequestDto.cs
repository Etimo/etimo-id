// ReSharper disable InconsistentNaming

using Etimo.Id.Api.Attributes;
using Etimo.Id.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Etimo.Id.Api.Applications
{
    public class ApplicationRequestDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [NqsChar]
        public string name { get; set; }

        [MaxLength(128)]
        [NqsChar]
        public string description { get; set; }

        [Required]
        [ValidValues("public", "confidential")]
        public string type { get; set; }

        [Base64]
        public string logo_base64 { get; set; }

        [Required]
        [ValidUri(allowFragment: true)]
        public string homepage_uri { get; set; }

        [Required]
        [ValidUri]
        public string redirect_uri { get; set; }

        [Range(5, 600)]
        public int authorization_code_lifetime_seconds { get; set; } = 180;

        [Range(1, 1440)]
        public int access_token_lifetime_minutes { get; set; } = 15;

        [Range(1, 90)]
        public int refresh_token_lifetime_days { get; set; } = 30;

        public bool allow_credentials_in_body                       { get; set; } = false;
        public bool allow_authorization_code_grant                  { get; set; } = true;
        public bool allow_client_credentials_grant                  { get; set; } = true;
        public bool allow_resource_owner_password_credentials_grant { get; set; } = false;
        public bool allow_implicit_grant                            { get; set; } = false;

        public Guid? user_id { get; set; }

        public Application ToApplication(int? applicationId = null)
            => new()
            {
                ApplicationId                              = applicationId ?? default,
                Name                                       = name,
                Description                                = description,
                Type                                       = type,
                LogoBase64                                 = logo_base64,
                HomepageUri                                = homepage_uri,
                RedirectUri                                = redirect_uri,
                AuthorizationCodeLifetimeSeconds           = authorization_code_lifetime_seconds,
                AccessTokenLifetimeMinutes                 = access_token_lifetime_minutes,
                RefreshTokenLifetimeDays                   = refresh_token_lifetime_days,
                AllowCredentialsInBody                     = allow_credentials_in_body,
                AllowAuthorizationCodeGrant                = allow_authorization_code_grant,
                AllowClientCredentialsGrant                = allow_client_credentials_grant,
                AllowResourceOwnerPasswordCredentialsGrant = allow_resource_owner_password_credentials_grant,
                AllowImplicitGrant                         = allow_implicit_grant,
                UserId                                     = user_id.GetValueOrDefault(),
            };
    }
}
