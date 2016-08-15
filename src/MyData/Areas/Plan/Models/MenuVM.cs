using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace MyData.Models
{
    public class MenuVM
    {
        public List<CIteration> Iterations { get; set; }
        public List<CProject> Projects { get; set; }

        public MenuVM()
        {
            Iterations = new List<CIteration>();
        }
    }
}