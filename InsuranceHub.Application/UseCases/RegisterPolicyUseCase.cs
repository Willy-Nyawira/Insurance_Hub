using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.UseCases
{
    public class RegisterPolicyUseCase
    {
        private readonly IPolicyRepository _policyRepository;

        public RegisterPolicyUseCase(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<Guid> Execute(CreatePolicyDto createPolicyDto, Guid userId)
        {
            var policy = new Policy
            {
                Id = Guid.NewGuid(),
                PolicyNumber = createPolicyDto.PolicyNumber,
                PolicyType = createPolicyDto.PolicyType,
                PremiumAmount = createPolicyDto.PremiumAmount,
                StartDate = createPolicyDto.StartDate,
                EndDate = createPolicyDto.EndDate,
                UserId = userId
            };

            await _policyRepository.AddAsync(policy);
            return  policy.Id;
        }
    }
}
