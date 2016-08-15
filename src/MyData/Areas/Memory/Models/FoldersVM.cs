using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.Models
{
    public class FoldersVM
    {
        public IList<FolderRowVM> Folders { get; set; }
    }

    public class FolderRowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}