using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyData.Models
{
    public class ProjectsVM
    {
        public List<ProjectWithTreeVM> Projects { get; set; }

        public ProjectsVM()
        {
            Projects = new List<ProjectWithTreeVM>();
        }
    }

    public class ProjectWithTreeVM
    {
        public CProject Project { get; set; }
        public int Number { get; set; }
        public int ParentNumber { get; set; }
    }
}