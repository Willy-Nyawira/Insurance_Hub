using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Enums;
using InsuranceHub.Domain.ValueObjects;

namespace InsuranceHub.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Email Email { get; set; }
        public UserCredentials Credentials { get; set; }
        public UserRole Roles { get; set; }
        public List<Policy> Policies { get; set; }
        

        public User()
        {
            Policies = new List<Policy>();
        }
    }
}
