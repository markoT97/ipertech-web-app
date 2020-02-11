using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class UserMessage : IValidation
    {
        public User User { get; set; }
        public Message Message { get; set; }

        public UserMessage()
        {
            User = new User();
            Message = new Message();
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
            if (!(!User.UserId.Equals(Guid.Empty) && !Message.MessageId.Equals(Guid.Empty)))
            {
                return false;
            }

            return true;
        }
    }
}
