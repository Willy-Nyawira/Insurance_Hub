using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.UseCases
{
    public class GetCustomerByIdUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Execute(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new Exception("Customer not found"); // Handle this as needed
            return customer;
        }
    }
}
