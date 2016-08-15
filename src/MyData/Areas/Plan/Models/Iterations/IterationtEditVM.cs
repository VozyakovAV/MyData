using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using System.Web.Mvc;

namespace MyData.Models
{
    public class IterationEditVM
    {
        public CIteration Iteration { get; set; }

        public IterationEditVM(CIteration iteration)
        {
            Iteration = iteration;
        }
    }
}