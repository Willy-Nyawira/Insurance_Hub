using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.Commands;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Domain.Exceptions;

namespace InsuranceHub.Application.Handlers
{
    public class LoginUserCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(LoginUserCommand command)
        {
            // Retrieve the user
            var user = await _userRepository.GetByUsernameAsync(command.Username);
            if (user == null || !_passwordHasher.VerifyPassword(command.Password, user.Credentials.PasswordHash))
            {
                throw new InvalidCredentialsException(command.Username);
            }

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
