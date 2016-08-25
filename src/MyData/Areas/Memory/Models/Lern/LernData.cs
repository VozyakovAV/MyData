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

        public AnswerResult CheckAnswer(int answerInt = 0, string answerSt = "")
        {
            if (CurrentQuestion == null)
                return null;
            if (CurrentQuestion.Question is TestQuestion)
            {
                var awr = CurrentQuestion.Answer as TestAnswer;
                var res = new TestAnswerResult();
                res.CorrectAnswer = awr.NumberAnswer;
                res.IsCorrect = awr.NumberAnswer == answerInt;
                AllowNextQuestion = true;
                return res;
            }
            if (CurrentQuestion.Question is WordQuestion)
            {
                var awr = CurrentQuestion.Answer as WordAnswer;
                var res = new WordAnswerResult();
                res.CorrectAnswer = awr.Answer;
                res.IsCorrect = awr.Answer == answerSt;
                AllowNextQuestion = true;
                return res;
            }
            return null;
        }
    }


    public class AnswerResult
    {
        public bool IsCorrect { get; set; }
    }

    public class TestAnswerResult : AnswerResult
    {
        public int CorrectAnswer { get; set; }
    }

    public class WordAnswerResult : AnswerResult
    {
        public string CorrectAnswer { get; set; }
    }
}