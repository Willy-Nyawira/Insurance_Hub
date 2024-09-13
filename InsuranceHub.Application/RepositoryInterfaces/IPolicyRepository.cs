using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.RepositoryInterfaces
{
    public interface IPolicyRepository
    {
        Task<Policy> GetPolicyByIdAsync(Guid policyId);
        Task<IEnumerable<Policy>> GetByCustomerIdAsync(Guid customerId);
        Task AddAsync(Policy policy);
        Task UpdateAsync(Policy policy);
        Task DeleteAsync(Guid id);
        
    }
}
