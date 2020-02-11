using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class OptionVoter : IValidation
    {
        public Guid UserId { get; set; }
        public Guid PollId { get; set; }
        public Guid PollOptionId { get; set; }

        public OptionVoter()
        {

        }

        public OptionVoter(Guid userId, Guid pollId = new Guid(), Guid pollOptionId = new Guid())
        {
            UserId = userId;
            PollId = pollId;
            PollOptionId = pollOptionId;
        }

        public override string ToString()
        {
            return UserId + ", " + PollId + ", " + PollOptionId;
        }

        public bool IsValid()
        {
            if (!(!UserId.Equals(Guid.Empty) && !PollId.Equals(Guid.Empty) & !PollOptionId.Equals(Guid.Empty)))
            {
                return false;
            }

            return true;
        }
    }
}
