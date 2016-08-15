using Domain;
using System.Collections.Generic;

namespace MyData.Models
{
    public class ProjectVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectRowVM> SubProjects { get; set; }
        public List<TaskRowVM> OpenTasks { get; set; }
        public List<TaskRowVM> ClosedTasks { get; set; }
    }

    public class ProjectRowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; }
        public CProjectStatus Status { get; set; }
    }
}