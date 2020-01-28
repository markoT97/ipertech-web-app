using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class Poll : IValidation
    {
        public Guid PollId { get; set; }
        public string Question { get; set; }
        public int NumberOfVoters { get; set; }

        public Poll()
        {
            PollId = Guid.NewGuid();
        }

        public Poll(Guid pollId, string question, int numberOfVoters)
        {
            PollId = pollId;
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
