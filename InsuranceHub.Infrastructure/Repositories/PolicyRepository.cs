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

        public async Task<IEnumerable<Policy>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _dbContext.PolicyCustomerAssociations
                .Where(pca => pca.CustomerId == customerId)
                .Include(pca => pca.Policy)
                .Select(pca => pca.Policy)
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
        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            return await _dbContext.Policies.ToListAsync();
        }
        public async Task<IEnumerable<PolicyCustomerAssociation>> GetAllPoliciesWithCustomersAsync()
        {
            return await _dbContext.PolicyCustomerAssociations
                .Include(pca => pca.Policy)
                .Include(pca => pca.Customer)
                .ToListAsync();
        }
        public async Task AddPolicyCustomerAssociationAsync(Guid policyId, Guid customerId)
        {
            var association = new PolicyCustomerAssociation
            {
                PolicyId = policyId,
                CustomerId = customerId
            };

            await _dbContext.PolicyCustomerAssociations.AddAsync(association);
            await _dbContext.SaveChangesAsync();
        }

    }


}

