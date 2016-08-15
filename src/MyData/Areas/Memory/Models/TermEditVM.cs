using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MyData.Models
{
    public class TermEditVM
    {
        public int Id { get; set; }

        [Display(Name = "Вопрос")]
        public string Question { get; set; }

        [Display(Name = "Ответ")]
        public string Answer { get; set; }

        [Display(Name = "Набор")]
        public int SetID { get; set; }
        public SelectList Sets { get; set; }
    }

}