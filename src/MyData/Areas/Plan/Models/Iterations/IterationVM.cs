using System;
using System.Collections.Generic;

namespace MyData.Models
{
    public class IterationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskRowVM> OpenTasks { get; set; }
        public List<TaskRowVM> ClosedTasks { get; set; }
    }
}