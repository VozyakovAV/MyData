using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyData.Models
{
    public class SetEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FolderID { get; set; }
        public SelectList Folders { get; set; }
    }
}