using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.DTOS
{
    public class CustomerRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public char Gender { get; set; }
        public string PhysicalAddress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
