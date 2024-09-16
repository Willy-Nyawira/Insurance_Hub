using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;
using System.Security.Claims;

namespace InsuranceHub.Application.UseCases
{
    public class RegisterPolicyUseCase
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public RegisterPolicyUseCase(IPolicyRepository policyRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _policyRepository = policyRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<Guid> Execute(CreatePolicyDto createPolicyDto, string username)
        {


            var loggedInUsername = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(loggedInUsername))
            {
                throw new InvalidOperationException("Username not found in the claims.");
            }

            // Assuming username is passed as a string and matches the format you need
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty");
            }

            var user = await _userRepository.FindByUsernameAsync(username); // Add this repository or similar method if not already present
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var policy = new Policy
            {
                PolicyType = createPolicyDto.PolicyType,
                PremiumAmount = createPolicyDto.PremiumAmount,
                StartDate = createPolicyDto.StartDate,
                EndDate = createPolicyDto.EndDate,
                UserId = user.Id, // Use the user ID associated with the username
                CreatedBy = loggedInUsername
            };

            // Auto-generate the policy number
            policy.GeneratePolicyNumber();

            await _policyRepository.AddAsync(policy);
            return  policy.Id;
        }
    }
}
