using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class CProject : CBaseEntity
    {
        public string           Name            { get; set; }
        public string           Description     { get; set; }
        public DateTime         DateCreated     { get; set; }
        public DateTime?        DateClosed      { get; set; }
        public DateTime?        Deadline        { get; set; }
        public CProjectStatus   Status          { get; set; }
        public bool             IsDeleted       { get; set; }

        public int? ParentID { get; set; }
        public CProject Parent { get; set; }
        public ICollection<CProject> Projects { get; set; }

        public ICollection<CTask> Tasks { get; set; }

        public CProject()
        {
            DateCreated = DateTime.Now;
            Tasks = new List<CTask>();
            Projects = new List<CProject>();
        }

        public CProject(string name) : this()
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum CProjectStatus
    {
        Open,
        Completed,
        Canceled
    }
}