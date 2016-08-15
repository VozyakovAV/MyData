using System;
using System.Collections.Generic;
using Domain;

namespace MyData.Models
{
    public class TaskRowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public string IterationName { get; set; }
        public string Deadline { get; set; }
        public CTaskStatus Status { get; set; }
    }
}