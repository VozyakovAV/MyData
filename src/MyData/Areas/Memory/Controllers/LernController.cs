using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using MyData.Models;

namespace MyData.Areas.Memory.Controllers
{
    public class LernController : BaseController
    {
        private const string DATA_SESSION = "LernData";

        public ActionResult Index(int setID = 0)
        {
            var data = mng.Lern.CreateLernData(setID);
            if (data == null)
                return HttpNotFound();

            Session[DATA_SESSION] = data;
            return View();
        }

        public JsonResult NextQuestion()
        {
            var data = Session[DATA_SESSION] as LernData;
            if (data == null)
                return Json(false);

            var res = data.NextQuestion();
            return Json(res);
        }

        public JsonResult CheckAnswerTest(int answer)
        {
            var data = Session[DATA_SESSION] as LernData;
            if (data == null)
                return Json(false);
            var res = data.CheckAnswer(answerInt: answer);
            return Json(res);
        }

        public JsonResult CheckAnswerWord(string answer)
        {
            var data = Session[DATA_SESSION] as LernData;
            if (data == null)
                return Json(false);
            var res = data.CheckAnswer(answerSt: answer);
            return Json(res);
        }
    }
}