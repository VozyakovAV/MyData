using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.Models
{
    public class TermsVM
    {
        public int SetID { get; set; }
        public string SetName { get; set; }
        public IList<TermRowVM> Terms { get; set; }
    }

    public class TermRowVM
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}