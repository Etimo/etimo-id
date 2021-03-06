using System;

namespace Etimo.Id.Entities.Abstractions
{
    public interface IClientCredentialsTokenRequest
    {
        Guid   ClientId          { get; }
        string ClientSecret      { get; }
        bool   CredentialsInBody { get; }
        string Scope             { get; }
    }
}
