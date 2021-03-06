using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etimo.Id.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IEtimoIdDbContext _dbContext;

        public ApplicationRepository(IEtimoIdDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Application>> GetAllAsync()
            => _dbContext.Applications.ToListAsync();

        public Task<List<Application>> GetByUserIdAsync(Guid userId)
            => _dbContext.Applications.Where(a => a.UserId == userId).ToListAsync();

        public Task<Application> FindAsync(int applicationId)
            => _dbContext.Applications.FindAsync(applicationId).AsTask();

        public Task<Application> FindAsync(Guid clientId)
            => _dbContext.Applications.FirstOrDefaultAsync(a => a.ClientId == clientId);

        public Task<Application> FindByClientIdAsync(Guid clientId)
            => _dbContext.Applications.FirstOrDefaultAsync(a => a.ClientId == clientId);

        public Task<bool> ExistsByClientIdAsync(Guid clientId)
            => _dbContext.Applications.AnyAsync(a => a.ClientId == clientId);

        public void Add(Application application)
            => _dbContext.Applications.Add(application);

        public void Delete(Application application)
        {
            if (application != null) { _dbContext.Applications.Remove(application); }
        }

        public Task<int> SaveAsync()
            => _dbContext.SaveChangesAsync();
    }
}
