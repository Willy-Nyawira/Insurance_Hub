using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.Commands
{
    public class RegisterCustomerCommand
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public char Gender { get; set; }
        public string PhysicalAddress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public RegisterCustomerCommand(string firstName, string lastName, string password, DateTime dob, char gender,
                                       string physicalAddress, string city, string region,
                                       string phoneNumber, string emailAddress)
        {
            
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Dob = dob;
            Gender = gender;
            PhysicalAddress = physicalAddress;
            City = city;
            Region = region;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }
    }
}

