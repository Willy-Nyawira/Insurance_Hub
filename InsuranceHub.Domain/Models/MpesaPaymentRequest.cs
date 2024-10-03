using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Domain.Models
{
    public class MpesaPaymentRequest
    {
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string AccountReference { get; set; }  // Add this property
        public string TransactionDesc { get; set; }
    }
}
