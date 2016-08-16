using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyData.BLL.Site
{
    public class ImportExportTermsManager : ManagerBase
    {
        public ImportExportTermsManager(ManagerSite manager) : base(manager)
        { }

        public List<TermSimple> ParseTerms(string text, string wordDelimeter, string rowDelimeter)
        {
            var result = new List<TermSimple>();
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(wordDelimeter) || string.IsNullOrEmpty(rowDelimeter))
                return result;

            text = text.Trim();
            var rows = text.Split(new string[] { rowDelimeter }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in rows)
            {
                var words = row.Trim().Split(new string[] { wordDelimeter }, StringSplitOptions.None);
                if (words.Length >= 2)
                {
                    var term = new TermSimple() { Question = words[0].Trim(), Answer = words[1].Trim() };
                    result.Add(term);
                }
            }
            return result;
        }

        public bool ImportTerms(int setID, string text, string wordDelimeter, string rowDelimeter)
        {
            try
            {
                var terms = ParseTerms(text, wordDelimeter, rowDelimeter);
                foreach (var term in terms)
                {
                    var newTerm = ManagerSite.Memory.NewTerm(term.Question);
                    newTerm.SetID = setID;
                    newTerm.Answer = term.Answer;
                    ManagerSite.Memory.Save(newTerm, false);
                }
                ManagerSite.SaveDataBase();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public class TermSimple
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
    }
}