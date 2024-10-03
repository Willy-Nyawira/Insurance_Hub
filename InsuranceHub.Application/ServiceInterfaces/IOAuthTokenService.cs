using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.ServiceInterfaces
{
    public interface IOAuthTokenService
    {
        Task<string> GetAccessTokenAsync();
    }
}
