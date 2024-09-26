using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Domain.Entities
{
    public class PolicyCustomerAssociation
    {
        public Guid Id { get; set; }
        public Guid PolicyId { get; set; } 
        public Guid CustomerId { get; set; } 
        public virtual Policy Policy { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
