using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyData.BLL.Site
{
    public class ImportExportTermsManager : ManagerBase
    {
        public ImportExportTermsManager(ManagerSite manager) : base(manager)
        { }

        public List<TermSimple> ParseTerms(string text, string wordDelimeter, string rowDelimeter, bool revertValues)
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
                    if (revertValues)
                    {
                        var t = term.Question;
                        term.Question = term.Answer;
                        term.Answer = t;
                    }
                    result.Add(term);
                }
            }
            return result;
        }

        public void ImportTerms(int setID, string text, string wordDelimeter, string rowDelimeter, bool revertValues)
        {
            var terms = ParseTerms(text, wordDelimeter, rowDelimeter, revertValues);
            foreach (var term in terms)
            {
                var newTerm = ManagerSite.Memory.NewTerm(term.Question, term.Answer);
                newTerm.SetID = setID;
                ManagerSite.Memory.Save(newTerm, false);
            }
            ManagerSite.SaveDataBase();
        }

        public string ExportTerms(int setID, string wordDelimeter, string rowDelimeter)
        {
            var terms = ManagerSite.Memory.GetTerms(setID);
            var sb = new StringBuilder();
            foreach(var term in terms)
            {
                sb.Append(term.Question);
                sb.Append(wordDelimeter);
                sb.Append(term.Answer);
                sb.AppendLine(rowDelimeter);
            }
            return sb.ToString();
        }

        public class TermSimple
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
    }
}