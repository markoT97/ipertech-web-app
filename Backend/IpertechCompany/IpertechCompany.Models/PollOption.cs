﻿using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class PollOption : IValidation
    {
        public Guid PollOptionId { get; set; }
        public Poll Poll { get; set; }
        public string AnswerText { get; set; }

        public PollOption()
        {
            Poll = new Poll();
        }

        public PollOption(Guid pollOptionId, Poll poll = null, string answerText = null)
        {
            PollOptionId = pollOptionId.Equals(Guid.Empty) ? Guid.NewGuid() : pollOptionId;
            Poll = poll;
            AnswerText = answerText;
        }

        public override string ToString()
        {
            return PollOptionId + ", " + Poll + ", " + AnswerText;
        }

        public bool IsValid()
        {
            if (!(!PollOptionId.Equals(Guid.Empty) && !Poll.Equals(null) && AnswerText != null))
            {
                return false;
            }

            return true;
        }
    }
}
