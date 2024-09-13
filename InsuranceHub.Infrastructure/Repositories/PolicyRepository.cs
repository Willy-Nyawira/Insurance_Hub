using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceHub.Infrastructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PolicyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Policy> GetPolicyByIdAsync(Guid policyId)
        {
            return await _dbContext.Policies.FindAsync(policyId);
        }

        public async Task<IEnumerable<Policy>> GetByCustomerIdAsync(Guid userId)
        {
            return await _dbContext.Policies
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Policy policy)
        {
            await _dbContext.Policies.AddAsync(policy);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Policy policy)
        {
            _dbContext.Policies.Update(policy);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid policyId)
        {
            var policy = await GetPolicyByIdAsync(policyId);
            if (policy != null)
            {
                _dbContext.Policies.Remove(policy);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
