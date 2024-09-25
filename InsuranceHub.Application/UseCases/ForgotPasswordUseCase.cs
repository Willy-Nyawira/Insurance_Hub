using System;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;

namespace InsuranceHub.Application.UseCases
{
    public class ForgotPasswordUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public ForgotPasswordUseCase(ICustomerRepository customerRepository, ITokenService tokenService, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task Execute(ForgotPasswordDTO command)
        {
            // Retrieve the user by email
            var user = await _customerRepository.GetByEmailAsync(command.Email);

            // Check if the email exists
            if (user == null)
            {
                throw new ArgumentException("No account found with the provided email address.");
            }

            // Generate a password reset token
            var token = _tokenService.GenerateResetToken(user.Id);

            // Send the reset token to the registered email
            await _emailService.SendPasswordResetEmail(user.EmailAddress.Address, token); // Fixed line
        }
    }
}
