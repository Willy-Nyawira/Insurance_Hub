using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Domain.ValueObjects
{
    public class Email
    {
        public string Address { get; private set; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || !address.Contains("@"))
                throw new ArgumentException("Invalid email address.");

            Address = address;
        }
        private Email()
        {
            // Required by EF Core for deserialization
        }
        public override string ToString()
        {
            return Address;
        }
            //=> Address;

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
        }
    }
}
