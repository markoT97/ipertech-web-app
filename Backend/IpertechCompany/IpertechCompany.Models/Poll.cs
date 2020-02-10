using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class Poll : IValidation
    {
        public Guid PollId { get; set; }
        public string Question { get; set; }
        public int NumberOfVoters { get; set; }

        public Poll()
        {

        }

        public Poll(Guid pollId, string question = null, int numberOfVoters = 0)
        {
            PollId = pollId.Equals(Guid.Empty) ? Guid.NewGuid() : pollId;
            Question = question;
            NumberOfVoters = numberOfVoters;
        }

        public override string ToString()
        {
            return PollId + ", " + Question + ", " + NumberOfVoters;
        }

        public bool IsValid()
        {
            if (!(!PollId.Equals(null) && Question != null))
            {
                return false;
            }

            return true;
        }
    }
}
