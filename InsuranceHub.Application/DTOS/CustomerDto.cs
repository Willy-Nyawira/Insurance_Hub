using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceHub.Application.DTOS
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        
        public DateTime Dob { get; set; }
        public char Gender { get; set; }
        public string PhysicalAddress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public List<PolicyDto> Policies { get; set; }

        public CustomerDto()
        {
            Policies = new List<PolicyDto>();
        }
    }
}
