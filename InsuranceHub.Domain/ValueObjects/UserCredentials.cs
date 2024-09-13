using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Domain.ValueObjects
{
    public class UserCredentials
    {
        public Guid Id { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
