using Etimo.Id.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etimo.Id.Abstractions
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAllAsync();
        Task<List<Application>> GetByUserIdAsync(Guid userId);
        Task<Application> FindAsync(int applicationId);
        Task<Application> FindAsync(Guid clientId);
        Task<Application> FindByClientIdAsync(Guid clientId);
        Task<bool> ExistsByClientIdAsync(Guid clientId);
        void Add(Application application);
        void Delete(Application application);
        Task<int> SaveAsync();
    }
}
