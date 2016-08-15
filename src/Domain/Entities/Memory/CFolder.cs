using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Memory
{
    public class CFolder : CBaseEntity
    {
        public string Name { get; set; }
        public ICollection<CSet> Sets { get; set; }

        public CFolder()
        {
            Sets = new List<CSet>();
        }

        public CFolder(string name) : this()
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}