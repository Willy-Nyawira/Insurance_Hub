using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Enums;

namespace InsuranceHub.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public UserRole RoleType { get; set; }
    }
}
