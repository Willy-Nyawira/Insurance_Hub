using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Application.Services;
using InsuranceHub.Domain.Entities;
using BCrypt.Net;
using InsuranceHub.Domain.Enums;


namespace InsuranceHub.Application.UseCases
{
    public class CustomerLoginUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public CustomerLoginUseCase(ICustomerRepository customerRepository, IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Execute(LoginDto loginDto)
        {
            var customer = await _customerRepository.GetByUsernameAsync(loginDto.Username);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            if (!_passwordHasher.VerifyPassword(loginDto.Password, customer.PasswordHash))
            {
                throw new ArgumentException("Invalid password.");
            }

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(new User
            {
                Username = customer.Username,
                Email = customer.EmailAddress,
                Roles = UserRole.Customer
            });

            return token;
        }
    }
}
