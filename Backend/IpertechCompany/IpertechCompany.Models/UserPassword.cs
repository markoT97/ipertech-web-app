using System;

namespace IpertechCompany.Models
{
    public class UserPassword
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }

        public UserPassword()
        {

        }

        public UserPassword(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public override string ToString()
        {
            return UserId + ", " + Password;
        }

        public bool IsValid()
        {
            if (!(!UserId.Equals(Guid.Empty) && Password != null))
            {
                return false;
            }

            return true;
        }
    }
}
