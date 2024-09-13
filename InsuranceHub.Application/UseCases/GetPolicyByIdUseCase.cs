using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.UseCases
{
    public class GetPolicyByIdUseCase
    {
        private readonly IPolicyRepository _policyRepository;

        public GetPolicyByIdUseCase(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<Policy> ExecuteAsync(Guid policyId)
        {
            // Validate the policy ID
            if (policyId == Guid.Empty)
            {
                throw new ArgumentException("Invalid policy ID.");
            }

            // Fetch the policy from the repository
            var policy = await _policyRepository.GetPolicyByIdAsync(policyId);

            // Handle case where the policy is not found
            if (policy == null)
            {
                throw new KeyNotFoundException("Policy not found.");
            }

            return policy;
        }
    }
}
