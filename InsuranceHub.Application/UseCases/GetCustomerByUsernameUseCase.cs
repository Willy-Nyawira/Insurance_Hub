using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.UseCases
{
    public class GetCustomerByUsernameUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByUsernameUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Execute(string username)
        {
            // Assuming your repository has a method for getting a customer by username
            var customer = await _customerRepository.GetByUsernameAsync(username);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            return customer;
        }
    }
}
