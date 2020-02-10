using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class User : IValidation
    {
        public Guid UserId { get; set; }
        public UserContract UserContract { get; set; }
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

        public User(Guid userId, UserContract userContract = null, string role = null, string firstName = null, string lastName = null, string gender = null, string email = null, string phoneNumber = null, string password = null
         , string imageLocation = null)
        {
            UserId = userId.Equals(Guid.Empty) ? Guid.NewGuid() : userId;
            UserContract = userContract;
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
            return UserId + ", " + UserContract + ", " + Role + ", " + FirstName + ", " + LastName + ", " + Gender + ", " + Email + ", " + PhoneNumber + ", " + Password + ", " + ImageLocation;
        }

        public bool IsValid()
        {

            if (!(!UserId.Equals(null) && !UserContract.Equals(null) && Role != null && FirstName != null &&
                  LastName != null && Email != null && Password != null))
            {
                return false;
            }

            return true;
        }
    }
}
