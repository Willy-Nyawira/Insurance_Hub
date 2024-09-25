using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;

namespace InsuranceHub.Application.UseCases
{
    public class DeletePolicyUseCase
    {
        private readonly IPolicyRepository _policyRepository;

        public DeletePolicyUseCase(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task ExecuteAsync(Guid policyId)
        {
            await _policyRepository.DeleteAsync(policyId);
        }
    }
}
