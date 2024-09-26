using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.DTOS
{
    public class PurchasePolicyDto
    {
        public Guid PolicyId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
