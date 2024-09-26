using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace InsuranceHub.Application.UseCases
{
    public class PurchasePolicyUseCase
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PurchasePolicyUseCase(IPolicyRepository policyRepository, IInvoiceRepository invoiceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _policyRepository = policyRepository;
            _invoiceRepository = invoiceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Invoice> Execute(Guid policyId)
        {
            // Retrieve logged-in user details
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name) ??
                              _httpContextAccessor.HttpContext.User.FindFirst("unique_name");

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User not logged in");
            }

            var customerUsername = userIdClaim.Value;

            // Fetch the policy

            var policy = await _policyRepository.GetPolicyByIdAsync(policyId);
            if (policy == null)
            {
                throw new ArgumentException("Policy not found");
            }

            decimal amountToCharge = CalculateAmount(policy);

            // Generate invoice based on payment frequency
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                CustomerUsername = customerUsername,
                PolicyId = policy.Id,
                PurchaseDate = DateTime.UtcNow,
                Amount = amountToCharge,
                PolicyType = policy.PolicyType,
                PaymentFrequency = policy.PaymentFrequency
            };

            // Save invoice
            await _invoiceRepository.CreateInvoiceAsync(invoice);
            // Assuming you have a method to get the customer ID by username
            var customerId = await _invoiceRepository.GetCustomerIdByUsernameAsync(customerUsername);
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("Customer ID not found");
            }


            await _policyRepository.AddPolicyCustomerAssociationAsync(policy.Id, customerId);


            return invoice;
        }

        private decimal CalculateAmount(Policy policy)
        {
            switch (policy.PaymentFrequency)
            {
                case PaymentFrequency.OneTime:
                    return policy.PremiumAmount; 
                case PaymentFrequency.Monthly:
                    return policy.PremiumAmount / 12; 
                case PaymentFrequency.Quarterly:
                    return policy.PremiumAmount / 4; 
                case PaymentFrequency.HalfYearly:
                    return policy.PremiumAmount / 2; 
                case PaymentFrequency.Annually:
                    return policy.PremiumAmount; 
                default:
                    throw new InvalidOperationException("Unknown payment frequency");
            }
        }
    }
}
