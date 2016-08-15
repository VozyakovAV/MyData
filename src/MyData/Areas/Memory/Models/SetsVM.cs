using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.Models
{
    public class SetsVM
    {
        public FolderVM Folder { get; set; }
        public IList<SetRowVM> Sets { get; set; }
    }

    public class SetRowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}