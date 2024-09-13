using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;

namespace InsuranceHub.Application.UseCases
{
    public class GetPoliciesByCustomerUseCase
    {
        private readonly IPolicyRepository _policyRepository;

        public GetPoliciesByCustomerUseCase(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<IEnumerable<PolicyDto>> Execute(Guid customerId)
        {
            var policies = await _policyRepository.GetByCustomerIdAsync(customerId);
            return policies.Select(policy => new PolicyDto
            {
                Id = policy.Id,
                PolicyNumber = policy.PolicyNumber,
                PolicyType = policy.PolicyType,
                PremiumAmount = policy.PremiumAmount,
                StartDate = policy.StartDate,
                EndDate = policy.EndDate,
               
                UserId = policy.UserId
            }).ToList();
        }

    }
}
