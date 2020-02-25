using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class Message : IValidation
    {
        public Guid MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Category { get; set; }

        public Message()
        {

        }

        public Message(Guid messageId, string title = null, string content = null, DateTime? createdAt = null, string category = null)
        {
            MessageId = messageId.Equals(Guid.Empty) ? Guid.NewGuid() : messageId;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            Category = category;
        }
        public bool IsValid()
        {
            if (!(!MessageId.Equals(Guid.Empty) && Title != null && Content != null && !CreatedAt.Equals(null) &&
                  Category != null))
            {
                return false;
            }

            return true;
        }
    }
}
