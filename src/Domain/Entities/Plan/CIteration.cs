using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain
{
    public class CIteration : CBaseEntity
    {
        public DateTime         DateStart       { get; set; }
        public DateTime         DateFinish      { get; set; }
        public CIterationStatus Status          { get; set; }
        public bool             IsDeleted       { get; set; }

        public ICollection<CTask> Tasks { get; set; }

        public CIteration()
        {
            Tasks = new List<CTask>();
        }

        public CIteration(DateTime start, DateTime finish) :this()
        {
            this.DateStart = start;
            this.DateFinish = finish;
        }

        public string Name
        {
            get
            {
                return string.Format("Итерация ({0}-{1})", DateStart.ToString("dd.MM"), DateFinish.ToString("dd.MM"));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum CIterationStatus
    {
        Open,
        Closed
    }
}