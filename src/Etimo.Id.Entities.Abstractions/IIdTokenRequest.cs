using System;

namespace Etimo.Id.Entities.Abstractions
{
    public interface IIdTokenRequest
    {
        string Code              { get; }
        Guid   ClientId          { get; }
        string ClientSecret      { get; }
        string RedirectUri       { get; }
        string Scope             { get; }
        bool   CredentialsInBody { get; }
    }
}
