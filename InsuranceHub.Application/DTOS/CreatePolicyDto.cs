using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.DTOS
{
    public class CreatePolicyDto
    {
        [Required]
        public string PolicyNumber { get; set; }

        [Required]
        public string PolicyType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium Amount must be greater than 0")]
        public decimal PremiumAmount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
