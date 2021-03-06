using Etimo.Id.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etimo.Id.Abstractions
{
    public interface IAddUserRoleRelationService
    {
        Task<List<Role>> AddRoleRelationAsync(Guid userId, Guid roleId);
    }
}
