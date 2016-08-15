using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using System.Web.Mvc;

namespace MyData.Models
{
    public class TaskEditVM
    {
        public CTask Task { get; set; }
        public SelectList Projects { get; set; }
        public SelectList Iterations { get; set; }

        public TaskEditVM(CTask task)
        {
            Task = task;
        }
    }
}