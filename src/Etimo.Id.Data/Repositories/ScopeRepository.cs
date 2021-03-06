using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etimo.Id.Data.Repositories
{
    public class ScopeRepository : IScopeRepository
    {
        private readonly IEtimoIdDbContext _dbContext;

        public ScopeRepository(IEtimoIdDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Scope>> GetAllAsync()
            => _dbContext.Scopes.ToListAsync();

        public Task<Scope> FindAsync(Guid scopeId)
            => _dbContext.Scopes.FindAsync(scopeId).AsTask();

        public void Add(Scope scope)
            => _dbContext.Scopes.Add(scope);

        public void Delete(Scope scope)
        {
            if (scope != null) { _dbContext.Scopes.Remove(scope); }
        }

        public Task<int> SaveAsync()
            => _dbContext.SaveChangesAsync();
    }
}
