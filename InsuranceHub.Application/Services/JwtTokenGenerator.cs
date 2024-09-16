using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace InsuranceHub.Application.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];

            // Check for null values
            if (string.IsNullOrEmpty(_secretKey))
            {
                throw new ArgumentNullException(nameof(_secretKey), "JWT SecretKey is not configured.");
            }

            if (string.IsNullOrEmpty(_issuer))
            {
                throw new ArgumentNullException(nameof(_issuer), "JWT Issuer is not configured.");
            }

            if (string.IsNullOrEmpty(_audience))
            {
                throw new ArgumentNullException(nameof(_audience), "JWT Audience is not configured.");
            }
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Username),  // Use user.Username here
            new Claim(ClaimTypes.Email, user.Email.Address),
            new Claim(ClaimTypes.Role, user.Roles.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
