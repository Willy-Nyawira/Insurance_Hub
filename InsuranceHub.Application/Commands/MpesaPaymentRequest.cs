using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.Commands
{
    public class MpesaPaymentRequest
    {
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
      
    }
}
