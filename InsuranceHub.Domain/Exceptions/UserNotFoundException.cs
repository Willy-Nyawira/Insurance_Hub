using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Domain.Exceptions
{
    public class UserNotFoundException:Exception
    {
        public UserNotFoundException(Guid userId)
        : base($"User with ID {userId} not found")
        {
            UserId = userId;
        }
        public Guid UserId { get; }
    }
}
