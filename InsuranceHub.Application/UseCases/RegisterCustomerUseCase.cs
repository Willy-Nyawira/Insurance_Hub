using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Domain.ValueObjects;

namespace InsuranceHub.Application.UseCases
{
    public class RegisterCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCustomerUseCase(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Execute(CustomerRegistrationDto dto)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,  
                LastName = dto.LastName,    
                Dob = dto.Dob,
                Gender = dto.Gender,
                PhysicalAddress = dto.PhysicalAddress,
                City = dto.City,
                Region = dto.Region,
                PhoneNumber = dto.PhoneNumber,
                EmailAddress = new Email(dto.EmailAddress),
                PasswordHash = _passwordHasher.Hash(dto.Password)
            };

            await _customerRepository.AddAsync(customer);
            return customer.Id;
        }
    }
}
