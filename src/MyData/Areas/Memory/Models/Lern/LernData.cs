using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyData.Models
{
    public class LernData
    {
        public bool AllowNextQuestion { get; private set; } = true;

        public Queue<TermQuestion> Questions = new Queue<TermQuestion>();
        public TermQuestion CurrentQuestion;

        public BaseQuestion NextQuestion()
        {
            if (Questions.Count == 0)
                CurrentQuestion = null;

            if (AllowNextQuestion && Questions.Count > 0)
                CurrentQuestion = Questions.Dequeue();

            AllowNextQuestion = false;
            return CurrentQuestion == null ? null : CurrentQuestion.Question;
        }

        public TestAnswerResult CheckAnswer(int answer)
        {
            if (CurrentQuestion == null)
                return null;
            if (CurrentQuestion.Question is TestQuestion)
            {
                var awr = CurrentQuestion.Answer as TestAnswer;
                var res = new TestAnswerResult();
                res.CorrectAnswer = awr.NumberAnswer;
                res.IsCorrect = awr.NumberAnswer == answer;
                AllowNextQuestion = true;
                return res;
            }
            return null;
        }

        
    }

    

    public class TestAnswerResult
    {
        public bool IsCorrect { get; set; }
        public int CorrectAnswer { get; set; }
    }
}