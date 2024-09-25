using System;
using System.Threading.Tasks;
using InsuranceHub.Application.Commands;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;

namespace InsuranceHub.Application.UseCases
{
    public class ResetPasswordUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordUseCase(ICustomerRepository customerRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task Execute(ResetPasswordCommand command)
        {
            // Verify the token
            var customerId = _tokenService.ValidateResetToken(command.Token);
            if (customerId == null)
            {
                throw new ArgumentException("Invalid or expired token.");
            }

            // Retrieve the customer
            var customer = await _customerRepository.GetByIdAsync(customerId.Value); // Use .Value to access the non-nullable Guid
            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            // Hash the new password
            var hashedPassword = _passwordHasher.Hash(command.NewPassword);

            // Update customer's password
            customer.PasswordHash = hashedPassword;
            await _customerRepository.UpdateAsync(customer);
        }
    }
}
