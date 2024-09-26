using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;

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
            var policyCustomerAssociations = await _policyRepository.GetAllPoliciesWithCustomersAsync();

            var policies = policyCustomerAssociations
                .Where(pca => pca.CustomerId == customerId)
                .Select(pca => new PolicyDto
                {
                    Id = pca.Policy.Id,
                    PolicyNumber = pca.Policy.PolicyNumber,
                    PolicyType = pca.Policy.PolicyType,
                    PremiumAmount = pca.Policy.PremiumAmount,
                    StartDate = pca.Policy.StartDate,
                    EndDate = pca.Policy.EndDate,
                    CustomerId = customerId,
                    UserName = pca.Policy.CreatedBy 
                }).ToList();

            return policies;
        }
    }
}
