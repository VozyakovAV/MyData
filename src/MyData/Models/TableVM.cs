using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.Models
{
    public class TableVM
    {
        public string Classes { get; set; }
        public string Title { get; set; }
        public IEnumerable<object> Items { get; set; }

        public TableVM(IEnumerable<object> items) : this(null, items, null) { }

        public TableVM(string title, IEnumerable<object> items) : this(title, items, null) { }

        public TableVM(IEnumerable<object> items, string classes) : this(null, items, classes) { }

        public TableVM(string title, IEnumerable<object> items, string classes)
        {
            this.Title = title;
            this.Items = items;
            this.Classes = classes;
        }
    }
}