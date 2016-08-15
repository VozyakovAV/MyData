using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.Models
{
    public class IterationsVM
    {
        public IList<IterationRowVM> Iterations { get; set; }
    }

    public class IterationRowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CIterationStatus Status { get; set; }
    }
}