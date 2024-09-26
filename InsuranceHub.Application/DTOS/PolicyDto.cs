using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Enums;

namespace InsuranceHub.Application.DTOS
{
    public class PolicyDto
    {
        public Guid Id { get; set; }
        public string PolicyNumber { get; set; }
        public PolicyType PolicyType { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CustomerId { get; set; }  
        public string UserName { get; set; }
    }
}
