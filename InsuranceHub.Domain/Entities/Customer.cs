using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.ValueObjects;

namespace InsuranceHub.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name is required.");
                _firstName = value;
                SetUsername();  // Update username whenever first or last name changes
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name is required.");
                _lastName = value;
                SetUsername();  // Update username whenever first or last name changes
            }
        }

        public string Username { get; private set; }

        private DateTime _dob;
        public DateTime Dob
        {
            get => _dob;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date of birth cannot be in the future.");
                _dob = value;
            }
        }

        private char _gender;
        public char Gender
        {
            get => _gender;
            set
            {
                if (value != 'M' && value != 'F')
                    throw new ArgumentException("Invalid gender value. Use 'M' for Male or 'F' for Female.");
                _gender = value;
            }
        }

        private string _physicalAddress;
        public string PhysicalAddress
        {
            get => _physicalAddress;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Physical address is required.");
                _physicalAddress = value;
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City is required.");
                _city = value;
            }
        }

        public string Region { get; set; }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number is required.");
                _phoneNumber = value;
            }
        }

        private Email _emailAddress;
        public Email EmailAddress
        {
            get => _emailAddress;
            set
            {
                if (string.IsNullOrWhiteSpace(value.Address))
                    throw new ArgumentException("Email address is required.");
                _emailAddress = value;
            }
        }

       
        public string PasswordHash { get; set; }
        public List<Policy> Policies { get; set; }

        public Customer()
        {
            Policies = new List<Policy>();
        }
        public Customer(Guid id, string firstName, string lastName, DateTime dob, char gender, string physicalAddress, string city, string region, string phoneNumber, Email emailAddress, string passwordHash)
            : this() // Calls the parameterless constructor to initialize Policies
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Dob = dob;
            Gender = gender;
            PhysicalAddress = physicalAddress;
            City = city;
            Region = region;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;

            // Set the username once first and last names are initialized
            SetUsername();
        }

            private void SetUsername()
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                Username = $"{FirstName}.{LastName}".ToLower();
            }
        }
    }
}
