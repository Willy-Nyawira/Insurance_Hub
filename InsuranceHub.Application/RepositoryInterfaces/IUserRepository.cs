using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<User> GetUserByIdAsync(Guid userId);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
    }
}
