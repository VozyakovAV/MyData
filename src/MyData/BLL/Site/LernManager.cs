using Common;
using Domain.Memory;
using MyData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyData.BLL.Site
{
    public class LernManager : ManagerBase
    {
        public const int COUNT_QUESTIONS = 100;
        public LernManager(ManagerSite manager) : base(manager)
        { }

        public LernData CreateLernData(int setID)
        {
            var set = ManagerSite.Memory.GetSet(setID);
            if (set == null)
                return null;

            var data = new LernData();

            var terms = ManagerSite.Memory.GetTerms(setID);
            var termsAll = terms.ToArray();
            terms.Shuffle();
            while (terms.Count > 0 && data.Questions.Count < COUNT_QUESTIONS)
            {
                var term = terms.First();
                var test = CreateTermQuestion(set, term, termsAll);
                data.Questions.Enqueue(test);
                terms.Remove(term);
            }

            return data;
        }

        private TermQuestion CreateTermQuestion(CSet set, CTerm term, IEnumerable<CTerm> terms)
        {
            var res = new TermQuestion();
            if (true)// term.Answer.Length > 10)
            {
                res.Question = CreateTestQuestion(set, term, terms);
                res.Answer = CreateTestAnswer(term, (TestQuestion)res.Question);
            }
            else
            {
                res.Question = CreateWordQuestion(set, term, terms);
                res.Answer = CreateWordAnswer(term, (WordQuestion)res.Question);
            }
            return res;
        }

        private TestQuestion CreateTestQuestion(CSet set, CTerm term, IEnumerable<CTerm> terms)
        {
            var res = new TestQuestion();
            res.SetID = set.Id;
            res.SetName = set.Name;
            res.Question = term.Question;

            var otherAnswers = terms.Where(x => x.Id != term.Id)
                .Where(x => x.Answer.ToLower() != x.Question.ToLower())
                .Select(x => x.Answer).Distinct().Take(3).ToList();

            var answers = new List<string>();
            answers.Add(term.Answer);
            answers.AddRange(otherAnswers);
            answers.Shuffle();
            res.Answers = answers.ToArray();

            return res;
        }

        private TestAnswer CreateTestAnswer(CTerm term, TestQuestion question)
        {
            var res = new TestAnswer();
            res.NumberAnswer = question.Answers.ToList().IndexOf(term.Answer);
            return res;
        }

        //

        private WordQuestion CreateWordQuestion(CSet set, CTerm term, IEnumerable<CTerm> terms)
        {
            var res = new WordQuestion();
            res.SetID = set.Id;
            res.SetName = set.Name;
            res.Question = term.Question;
            return res;
        }

        private WordAnswer CreateWordAnswer(CTerm term, WordQuestion question)
        {
            var res = new WordAnswer();
            res.Answer = term.Answer;
            return res;
        }
    }
}