using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class User : IValidation
    {
        public Guid UserId { get; set; }
        public Guid UserContractId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ImageLocation { get; set; }

        public User()
        {
        }

        public User(Guid userId, Guid userContractId, string role, string firstName, string lastName, string gender, string email, string phoneNumber, string password
         , string imageLocation)
        {
            UserId = userId;
            UserContractId = userContractId;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            ImageLocation = imageLocation;

        }

        public override string ToString()
        {
            return UserId + ", " + UserContractId + ", " + Role + ", " + FirstName + ", " + LastName + ", " + Gender + ", " + Email + ", " + PhoneNumber + ", " + Password + ", " + ImageLocation;
        }

        public bool IsValid()
        {

            if (!(!UserId.Equals(null) && !UserContractId.Equals(null) && Role != null && FirstName != null &&
                  LastName != null && Email != null && Password != null))
            {
                return false;
            }

            return true;
        }
    }
}
