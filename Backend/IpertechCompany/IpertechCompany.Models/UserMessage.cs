using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class UserMessage : IValidation
    {
        public User User { get; set; }
        public Message Message { get; set; }

        public UserMessage()
        {

        }

        public UserMessage(User user, Message message)
        {
            User = user;
            Message = message;
        }

        public override string ToString()
        {
            return User + ", " + Message;
        }

        public bool IsValid()
        {
            if (!(!User.Equals(null) && !Message.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
