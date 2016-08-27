using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyData.Models
{
    public class TermQuestion
    {
        public BaseQuestion Question { get; set; }
        public BaseAnswer Answer { get; set; }
    }

    public abstract class BaseQuestion
    {
        public abstract string Type { get; }
        public int SetID { get; set; }
        public string SetName { get; set; }
    }

    public abstract class BaseAnswer
    { }

    public class WordQuestion : BaseQuestion
    {
        public string Question { get; set; }

        public override string Type
        {
            get { return "word"; }
        }
    }

    public class WordAnswer : BaseAnswer
    {
        public string Answer { get; set; }
    }

    public class TestQuestion : BaseQuestion
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public override string Type
        {
            get { return "test"; }
        }
    }

    public class TestAnswer : BaseAnswer
    {
        public int NumberAnswer { get; set; }
    }
}