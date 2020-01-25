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
        public DateTime Timestamp { get; set; }
        public string Category { get; set; }

        public Message()
        {

        }

        public Message(Guid messageId, string title, string content, DateTime timestamp, string category)
        {
            MessageId = messageId;
            Title = title;
            Content = content;
            Timestamp = timestamp;
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
