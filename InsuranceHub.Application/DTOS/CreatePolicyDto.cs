using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Enums;

namespace InsuranceHub.Application.DTOS
{
    public class CreatePolicyDto
    {
       
        

        [Required]
        public PolicyType PolicyType { get; set; }
        [Required]
        public string Category { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium Amount must be greater than 0")]
        public decimal PremiumAmount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public PaymentFrequency PaymentFrequency { get; set; }

    }
}
