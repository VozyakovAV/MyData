using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using System.Web.Mvc;

namespace MyData.Models
{
    public class ProjectEditVM
    {
        public CProject Project { get; set; }
        public SelectList Projects { get; set; }

        public ProjectEditVM(CProject project)
        {
            Project = project;
        }
    }
}