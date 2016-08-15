using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using MyData.BLL.Site;
using Domain.Memory;

namespace MyData.Tests
{
    public class MemoryTests : BaseTest
    {
        public override void CleanDB()
        {
            FlushDB();
            CleanSets();
            CleanFolders();
            FlushDB();
        }

        public void CleanSets()
        {
            var sets = mng.Memory.GetSets();
            foreach (var set in sets)
                mng.Memory.Delete(set);
        }

        public void CleanFolders()
        {
            var folders = mng.Memory.GetFolders();
            foreach (var folder in folders)
                mng.Memory.Delete(folder);
        }

        #region folders

        public CFolder CreateFolder(string name = "Папка")
        {
            var folder = NewFolder(name);
            mng.Memory.Save(folder);
            return folder;
        }

        public CFolder NewFolder(string name = "Папка")
        {
            return mng.Memory.NewFolder(name);
        }

        public CFolder GetFolder(int id)
        {
            return mng.Memory.GetFolder(id);
        }

        public IList<CFolder> GetAllFolders()
        {
            return mng.Memory.GetFolders();
        }

        public void SaveFolder(CFolder folder)
        {
            mng.Memory.Save(folder);
        }

        public bool DeleteFolder(int id)
        {
            var folder = mng.Memory.GetFolder(id);
            return mng.Memory.Delete(folder);
        }

        public bool DeleteFolders(IEnumerable<CFolder> folders)
        {
            return DeleteFolders(folders.ToArray());
        }

        public bool DeleteFolders(params CFolder[] folders)
        {
            foreach (var folder in folders)
                mng.Memory.Delete(folder);
            return true;
        }

        public void DeleteAllFolders()
        {
            DeleteFolders(mng.Memory.GetFolders());
        }

        #endregion

        #region sets

        public CSet CreateSet()
        {
            var folder = CreateFolder();
            return CreateSet(folder);
        }

        public CSet CreateSet(CFolder folder)
        {
            var set = NewSet();
            set.Folder = folder;
            mng.Memory.Save(set);
            return set;
        }

        public CSet NewSet(string name = "Набор")
        {
            return mng.Memory.NewSet(name);
        }

        public CSet GetSet(int id)
        {
            return mng.Memory.GetSet(id);
        }

        public IList<CSet> GetAllSets()
        {
            return mng.Memory.GetSets();
        }

        public void SaveSet(CSet set)
        {
            mng.Memory.Save(set);
        }

        public bool DeleteSet(int id)
        {
            var set = mng.Memory.GetSet(id);
            return mng.Memory.Delete(set);
        }

        public bool DeleteSets(IEnumerable<CSet> sets)
        {
            return DeleteSets(sets.ToArray());
        }

        public bool DeleteSets(params CSet[] sets)
        {
            foreach (var set in sets)
                mng.Memory.Delete(set);
            return true;
        }

        public void DeleteAllSets()
        {
            DeleteSets(mng.Memory.GetSets());
        }

        #endregion

        #region terms

        public CTerm CreateTerm(string name = "Что такое термин?", CSet set = null)
        {
            var term = NewTerm();

            if (set == null)
                set = CreateSet();

            term.Set = set;
            mng.Memory.Save(term);
            return term;
        }

        public CTerm NewTerm(string name = "Что такое термин?")
        {
            return mng.Memory.NewTerm(name);
        }

        public CTerm GetTerm(int id)
        {
            return mng.Memory.GetTerm(id);
        }

        public IList<CTerm> GetAllTerms(int setID)
        {
            return mng.Memory.GetTerms(setID);
        }

        public void SaveTerm(CTerm term)
        {
            mng.Memory.Save(term);
        }

        public bool DeleteTerm(int id)
        {
            var term = mng.Memory.GetTerm(id);
            return mng.Memory.Delete(term);
        }

        public bool DeleteTerms(IEnumerable<CTerm> terms)
        {
            return DeleteTerms(terms.ToArray());
        }

        public bool DeleteTerms(params CTerm[] terms)
        {
            foreach (var term in terms)
                mng.Memory.Delete(term);
            return true;
        }


        #endregion
    }
}
