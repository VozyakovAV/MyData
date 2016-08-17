using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Memory
{
    public class CTerm : CBaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int SetID { get; set; }
        public CSet Set { get; set; }

        public CTerm()
        {
        }

        public CTerm(string question, string answer) : this()
        {
            this.Question = question;
            this.Answer = answer;
        }

        public override string ToString()
        {
            return Question;
        }
    }
}