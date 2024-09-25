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
    public class RegisterUserCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterUserCommand command)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByUsernameAsync(command.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }
            var salt = _passwordHasher.GetSalt();
            // Hash the password
            var hashedPassword = _passwordHasher.Hash(command.Password);

            // Create a new user
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = command.Username,
                Email = new Email(command.Email),
                Credentials = new UserCredentials
                {
                    PasswordHash = hashedPassword,
                    Salt=salt
                   // Salt = _passwordHasher.GetSalt()
                },
                Roles = command.Role,
                Policies = new List<Policy>()
            };

            // Save the user
            await _userRepository.AddAsync(newUser);
            
        }
    }
}
