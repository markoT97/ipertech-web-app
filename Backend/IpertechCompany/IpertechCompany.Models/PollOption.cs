using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    class PollOption : IValidation
    {
        public Guid PollOptionId { get; set; }
        public Guid PollId { get; set; }
        public string AnswerText { get; set; }

        public PollOption()
        {

        }

        public PollOption(Guid pollOptionId, Guid pollId, string answerText)
        {
            PollOptionId = pollOptionId;
            PollId = pollId;
            AnswerText = answerText;
        }

        public override string ToString()
        {
            return PollOptionId + ", " + PollId + ", " + AnswerText;
        }

        public bool IsValid()
        {
            if (!(!PollOptionId.Equals(null) && !PollId.Equals(null) && AnswerText != null))
            {
                return false;
            }

            return true;
        }
    }
}
