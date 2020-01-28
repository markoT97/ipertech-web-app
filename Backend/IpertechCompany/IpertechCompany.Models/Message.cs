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
        public DateTime CreatedAt { get; set; }
        public string Category { get; set; }

        public Message()
        {
            MessageId = Guid.NewGuid();
        }

        public Message(Guid messageId, string title, string content, DateTime createdAt, string category)
        {
            MessageId = messageId;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            Category = category;

        }
        public bool IsValid()
        {
            if (!(!MessageId.Equals(null) && Title != null && Content != null && !Timestamp.Equals(null) &&
                  Category != null))
            {
                return false;
            }

            return true;
        }
    }
}
