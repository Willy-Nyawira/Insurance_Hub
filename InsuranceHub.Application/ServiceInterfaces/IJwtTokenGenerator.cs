using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.ServiceInterfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
