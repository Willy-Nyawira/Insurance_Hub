using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Enums;

namespace InsuranceHub.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string CustomerUsername { get; set; }
        public Guid PolicyId { get; set; } 
        public DateTime PurchaseDate { get; set; }
        public decimal Amount { get; set; }
        public string CheckoutRequestId { get; set; }  
        public string MpesaReceiptNumber { get; set; } 
        public decimal AmountPaid { get; set; }    
        public DateTime PaymentDate { get; set; }

        public PolicyType PolicyType { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
