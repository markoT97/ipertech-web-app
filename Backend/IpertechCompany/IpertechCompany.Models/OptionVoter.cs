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

        public OptionVoter(Guid userId, Guid pollId, Guid pollOptionId)
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
            if (!(!UserId.Equals(null) && !PollId.Equals(null) & !PollOptionId.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
