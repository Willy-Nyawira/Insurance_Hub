using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;

namespace InsuranceHub.Application.UseCases
{
    public class UpdatePolicyUseCase
    {
        private readonly IPolicyRepository _policyRepository;

        public UpdatePolicyUseCase(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task ExecuteAsync(UpdatePolicyDto updatePolicyDto)
        {
            var policy = await _policyRepository.GetPolicyByIdAsync(updatePolicyDto.Id);
            if (policy != null)
            {
                policy.PolicyType = updatePolicyDto.PolicyType;
                policy.PremiumAmount = updatePolicyDto.PremiumAmount;
                policy.StartDate = updatePolicyDto.StartDate;
                policy.EndDate = updatePolicyDto.EndDate;

                await _policyRepository.UpdateAsync(policy);
            }
        }
    }
}
