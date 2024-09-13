using System;
using System.Collections.Generic;
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
        public string PolicyType { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }  
        public User User { get; set; }
    }
}
