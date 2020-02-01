using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class Message : IValidation
    {
        public Guid MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        private DateTime CreatedAt { get; set; }
        public string Category { get; set; }

        public Message()
        {
         
        }

        public Message(Guid messageId, string title = null, string content = null, string category = null)
        {
            MessageId = messageId.Equals(Guid.Empty) ? Guid.NewGuid() : messageId;
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            Category = category;
        }
        public bool IsValid()
        {
            if (!(!MessageId.Equals(null) && Title != null && Content != null && !CreatedAt.Equals(null) &&
                  Category != null))
            {
                return false;
            }

            return true;
        }
    }
}
