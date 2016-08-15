using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace MyData.Models
{
    public class MemoryMenuVM
    {
        public List<FolderVM> Folders { get; set; }

        public MemoryMenuVM()
        {
            Folders = new List<FolderVM>();
        }
    }

    public class FolderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}