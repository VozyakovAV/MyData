using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class CTask : CBaseEntity
    {
        public string           Name            { get; set; }
        public string           Description     { get; set; }
        public DateTime         DateCreated     { get; set; }
        public DateTime?        DateClosed      { get; set; }
        public DateTime?        Deadline        { get; set; }
        public CTaskStatus      Status          { get; set; }
        public bool             IsDeleted       { get; set; }

        public int? ProjectID { get; set; }
        public CProject Project { get; set; }

        public int? IterationID { get; set; }
        public CIteration Iteration { get; set; }

        public CTask()
        {
            DateCreated = DateTime.Now;
        }

        public CTask(string name) : this()
        {
            this.Name = name;
        }

        public void AddProject(CProject project)
        {
            if (project != null)
            {
                this.ProjectID = project.Id;
                this.Project = project;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum CTaskStatus
    {
        Open,
        Completed,
        Canceled
    }
}