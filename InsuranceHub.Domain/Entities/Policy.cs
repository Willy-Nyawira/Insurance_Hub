using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Enums;

namespace InsuranceHub.Domain.Entities
{
    public class Policy
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string PolicyNumber { get; set; }
        public PolicyType PolicyType { get; set; }
        public String Category { get; set; } 
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public string CreatedBy { get; set; }
        public User User { get; set; }


        public PaymentFrequency PaymentFrequency { get; set; }
        public bool IsActive { get; set; } = true;

        // Generate policy number with a pattern
        public void GeneratePolicyNumber()
        {
            // Example: "POL-20240916-0001"
            PolicyNumber = $"POL-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(1000, 9999)}";
        }
        public bool IsPolicyActive()
        {
            return DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        }
    }
}
