using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer> GetByIdAsync(Guid customerId);
        Task<Customer> GetByUsernameAsync(string username);

        Task<IEnumerable<Customer>> GetAllAsync();
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid customerId);
    }
}
