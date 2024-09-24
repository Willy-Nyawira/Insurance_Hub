using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.Commands;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Domain.ValueObjects;

namespace InsuranceHub.Application.Handlers
{
    public class RegisterCustomerCommandHandler
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterCustomerCommand command)
        {
            if (string.IsNullOrEmpty(command.FirstName) || string.IsNullOrEmpty(command.LastName))
            {
                throw new ArgumentException("First name and last name cannot be null or empty.");
            }
            string username = $"{command.FirstName}.{command.LastName}".ToLower();
            var existingCustomer = await _customerRepository.GetByUsernameAsync(username);
            if (existingCustomer != null)
            {
                throw new Exception("Username already exists.");
            }

            // Hash the password
            var hashedPassword = _passwordHasher.Hash(command.Password);

            // Create a new customer
            var customer = new Customer
            {
                
                FirstName=command.FirstName,
                LastName=command.LastName,
                Dob = command.Dob,
                Gender = command.Gender,
                PhysicalAddress = command.PhysicalAddress,
                City = command.City,
                Region = command.Region,
                PhoneNumber = command.PhoneNumber,
                EmailAddress = new Email(command.EmailAddress),
                PasswordHash = hashedPassword,
                
                
            };

            // Save the customer
            await _customerRepository.AddAsync(customer);
        }
    }
}
