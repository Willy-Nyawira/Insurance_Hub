using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Models;

namespace InsuranceHub.Application.ServiceInterfaces
{
    public interface IMpesaPaymentService
    {
        Task<string> InitiateStkPushAsync(StkPushRequest request);
    }
}
