using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Domain.Exceptions
{
    public class InvalidCredentialsException:Exception
    {
        public InvalidCredentialsException(string username)
       : base("Invalid credentials provided")
        {
            Username = username;
        }
        public string Username { get; }
    }
}
