using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class UserMessage : IValidation
    {
        public Guid UserId { get; set; }
        public Guid MessageId { get; set; }

        public UserMessage()
        {

        }

        public UserMessage(Guid userId, Guid messageId)
        {
            UserId = userId;
            MessageId = messageId;
        }

        public override string ToString()
        {
            return UserId + ", " + MessageId;
        }

        public bool IsValid()
        {
            if (!(!UserId.Equals(null) && !MessageId.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
