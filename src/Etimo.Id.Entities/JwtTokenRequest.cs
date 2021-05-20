using Etimo.Id.Entities.Abstractions;
using System;
using System.Collections.Generic;

namespace Etimo.Id.Entities
{
    public class JwtTokenRequest : IJwtTokenRequest
    {
        public string       Subject         { get; set; }
        public List<string> Audience        { get; set; }
        public Guid         ClientId        { get; set; }
        public string       Username        { get; set; }
        public string       Scope           { get; set; }
        public int          LifetimeMinutes { get; set; }
        public string       Nonce           { get; set; }
    }
}
