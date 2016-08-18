using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace MyData.Areas.Memory.Controllers
{
    public class LernController : BaseController
    {
        public ActionResult Index(int setID = 0)
        {
            var set = mng.Memory.GetSet(setID);
            if (set == null)
                return HttpNotFound();
            var data = new TestData() { SetID = set.Id };
            Session[DATA_SESSION] = data;
            
            return View();
        }

        private const string DATA_SESSION = "TestData";
        public JsonResult NextQuestion()
        {
            var data = Session[DATA_SESSION] as TestData;
            if (data == null)
                return Json(false);
            var terms = mng.Memory.GetTerms(data.SetID);
            if (terms.Count == 0)
                return Json(false);
            terms.Shuffle();
            var term = terms.First();
            var otherAnswers = terms.Where(x => x.Id != term.Id).Select(x => x.Question).Distinct().Take(3).ToList();

            var q = new TestQuestion();
            q.Question = term.Answer;
            var answers = new List<string>();
            answers.Add(term.Question);
            answers.AddRange(otherAnswers);
            answers.Shuffle();
            q.Answers = answers.ToArray();
            data.CorrectAnswer = answers.IndexOf(term.Question);

            return Json(q);
        }

        public JsonResult CheckAnswer(int answer)
        {
            var data = Session[DATA_SESSION] as TestData;
            if (data == null)
                return Json(false);

            var res = new CheckAnswerResult();
            res.CorrectAnswer = data.CorrectAnswer;
            if (data.CorrectAnswer == answer)
                res.IsCorrect = true;
            data.CheckAnswer();

            return Json(res);
        }
    }

    public class TestData
    {
        public int SetID { get; set; }

        private int _correctAnswer;
        public int CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; _isChecked = false; }
        }

        private bool _isChecked;
        public bool IsChecked { get { return _isChecked; } }

        public void CheckAnswer()
        {
            _isChecked = true;
        }
    }

    public class TestQuestion
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
    }

    public class CheckAnswerResult
    {
        public bool IsCorrect { get; set; }
        public int CorrectAnswer { get; set; }
    }
}