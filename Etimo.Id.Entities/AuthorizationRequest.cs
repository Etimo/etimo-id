using System;

namespace Etimo.Id.Entities
{
    /// <summary>
    /// This data object is used in the authorization code grant flow.
    /// </summary>
    public class AuthorizationRequest
    {
        public AuthorizationRequest(string responseType, Guid clientId, string state, string scope = null, string redirectUri = null)
        {
            ResponseType = responseType;
            ClientId = clientId;
            State = state;
            RedirectUri = redirectUri;
            Scope = scope;
        }

        public AuthorizationRequest(string responseType, Guid clientId, Guid codeId, string state, string scope = null, string redirectUri = null)
            : this(responseType, clientId, state, scope, redirectUri)
        {
            AuthorizationCodeId = codeId;
        }

        public string ResponseType { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? AuthorizationCodeId { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}