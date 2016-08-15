using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Memory
{
    public class CSet : CBaseEntity
    {
        public string Name { get; set; }
        public int FolderID { get; set; }
        public CFolder Folder { get; set; }
        public ICollection<CTerm> Questions { get; set; }

        public CSet()
        {
            Questions = new List<CTerm>();
        }

        public CSet(string name) : this()
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}