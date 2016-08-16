using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyData.Models
{
    public class ImportVM
    {
        public int SetID { get; set; }
        public string SetName { get; set; }
        public string WordDelimeter { get; set; }
        public string RowDelimeter { get; set; }
        public string Text { get; set; }


        public string ImportResult
        {
            get
            {
                return WordDelimeter + RowDelimeter;
            }
        }
    }
}