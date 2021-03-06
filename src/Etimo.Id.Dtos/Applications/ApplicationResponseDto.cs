// ReSharper disable InconsistentNaming

using Etimo.Id.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Etimo.Id.Dtos
{
    public class ApplicationResponseDto
    {
        public int                   application_id                                  { get; set; }
        public string                name                                            { get; set; }
        public string                description                                     { get; set; }
        public string                type                                            { get; set; }
        public string                logo_base64                                     { get; set; }
        public string                homepage_uri                                    { get; set; }
        public List<string>          redirect_uris                                   { get; set; }
        public int                   failed_logins_before_locked                     { get; set; }
        public int                   failed_logins_lock_lifetime_minutes             { get; set; }
        public int                   authorization_code_lifetime_seconds             { get; set; }
        public int                   access_token_lifetime_minutes                   { get; set; }
        public int                   refresh_token_lifetime_days                     { get; set; }
        public bool                  allow_credentials_in_body                       { get; set; }
        public bool                  allow_custom_query_parameters_in_redirect_uri   { get; set; }
        public bool                  allow_authorization_code_grant                  { get; set; }
        public bool                  allow_client_credentials_grant                  { get; set; }
        public bool                  allow_resource_owner_password_credentials_grant { get; set; }
        public bool                  allow_implicit_grant                            { get; set; }
        public bool                  generate_refresh_token_for_authorization_code   { get; set; }
        public bool                  generate_refresh_token_for_client_credentials   { get; set; }
        public bool                  generate_refresh_token_for_password_credentials { get; set; }
        public bool                  generate_refresh_token_for_implicit_flow        { get; set; }
        public Guid                  client_id                                       { get; set; }
        public Guid                  user_id                                         { get; set; }
        public DateTime              created_date                                    { get; set; }
        public DateTime              modified_date                                   { get; set; }
        public List<RoleResponseDto> roles                                           { get; set; }

        public static ApplicationResponseDto FromApplication(Application application)
            => FromApplication(application, true);

        public static ApplicationResponseDto FromApplication(Application application, bool includeChildren)
        {
            var dto = new ApplicationResponseDto
            {
                application_id                                  = application.ApplicationId,
                name                                            = application.Name,
                description                                     = application.Description,
                type                                            = application.Type,
                logo_base64                                     = application.LogoBase64,
                homepage_uri                                    = application.HomepageUri,
                redirect_uris                                   = application.RedirectUri.Split(" ").ToList(),
                failed_logins_before_locked                     = application.FailedLoginsBeforeLocked,
                failed_logins_lock_lifetime_minutes             = application.FailedLoginsLockLifetimeMinutes,
                authorization_code_lifetime_seconds             = application.AuthorizationCodeLifetimeSeconds,
                access_token_lifetime_minutes                   = application.AccessTokenLifetimeMinutes,
                refresh_token_lifetime_days                     = application.RefreshTokenLifetimeDays,
                allow_credentials_in_body                       = application.AllowCredentialsInBody,
                allow_custom_query_parameters_in_redirect_uri   = application.AllowCustomQueryParametersInRedirectUri,
                allow_authorization_code_grant                  = application.AllowAuthorizationCodeGrant,
                allow_client_credentials_grant                  = application.AllowClientCredentialsGrant,
                allow_resource_owner_password_credentials_grant = application.AllowResourceOwnerPasswordCredentialsGrant,
                allow_implicit_grant                            = application.AllowImplicitGrant,
                generate_refresh_token_for_authorization_code   = application.GenerateRefreshTokenForAuthorizationCode,
                generate_refresh_token_for_client_credentials   = application.GenerateRefreshTokenForClientCredentials,
                generate_refresh_token_for_password_credentials = application.GenerateRefreshTokenForPasswordCredentials,
                generate_refresh_token_for_implicit_flow        = application.GenerateRefreshTokenForImplicitFlow,
                client_id                                       = application.ClientId,
                user_id                                         = application.UserId,
                created_date                                    = application.CreatedDateTime,
                modified_date                                   = application.ModifiedDateTime,
            };

            if (includeChildren) { dto.roles = application.Roles?.Select(r => RoleResponseDto.FromRole(r, true)).ToList(); }

            return dto;
        }
    }
}
