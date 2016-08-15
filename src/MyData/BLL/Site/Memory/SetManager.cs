using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MyData.Models;
using Domain;
using Domain.Memory;
using AutoMapper;

namespace MyData.BLL.Site
{
    public partial class MemoryManager : ManagerBase
    {
        SetRepository _repoSet;

        #region base operations

        public bool Save(CSet item, bool withCommit = true)
        {
            _repoSet.Save(item);
            if (withCommit)
                ManagerSite.SaveDataBase();
            return true;
        }

        public bool Delete(CSet item)
        {
            _repoSet.Delete(item);
            ManagerSite.SaveDataBase();
            return true;
        }

        #endregion

        #region queries

        public CSet GetSet(int id)
        {
            return _repoSet.GetById(id);
        }

        public List<CSet> GetSets()
        {
            return _repoSet.GetAll().ToList();
        }

        public List<CSet> GetSets(int folderID)
        {
            return _repoSet.GetAll().Where(x => x.FolderID == folderID).ToList();
        }

        #endregion

        public CSet NewSet(string name)
        {
            return new CSet(name);
        }

        public CSet CreateSet(CSet setPattern)
        {
            var set = NewSet(setPattern.Name);
            set.FolderID = setPattern.FolderID;
            Save(set);
            return set;
        }

        public CSet EditSet(CSet setPattern)
        {
            var set = GetSet(setPattern.Id);
            if (set != null)
            {
                set.Name = setPattern.Name;
                set.FolderID = setPattern.FolderID;
                Save(set);
            }
            return set;
        }

        public SetsVM CreateSetsVM(int folderID)
        {
            var folder = GetFolder(folderID);
            var vm = new SetsVM();
            if (folder != null)
            {
                var sets = GetSets(folderID);
                vm.Folder = Mapper.Map<FolderVM>(folder);
                vm.Sets = Mapper.Map<List<SetRowVM>>(sets);
            }
            return vm;
        }

        public SetEditVM CreateSetEditVM(int folderID)
        {
            var set = NewSet("");
            set.FolderID = folderID;
            return CreateSetEditVM(set);
        }

        public SetEditVM CreateSetEditVM(CSet set)
        {
            var res = Mapper.Map<SetEditVM>(set);
            var folders = GetFolders();
            res.FolderID = set.FolderID;
            res.Folders = new SelectList(folders, "Id", "Name", set.Folder);
            return res;
        }
    }
}