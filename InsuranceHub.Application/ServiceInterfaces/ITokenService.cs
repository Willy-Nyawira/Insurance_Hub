using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.ServiceInterfaces
{
    public interface ITokenService
    {
        string GenerateResetToken(Guid customerId);
        Guid? ValidateResetToken(string token);
    }
}
