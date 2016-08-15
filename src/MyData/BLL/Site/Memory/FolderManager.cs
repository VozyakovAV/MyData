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
        FolderRepository _repoFolder;

        public MemoryManager(ManagerSite manager, FolderRepository repoFolder, SetRepository repoSet, 
            TermRepository repoQuestion) : base(manager)
        {
            this._repoFolder = repoFolder;
            this._repoSet = repoSet;
            this._repoTerm = repoQuestion;
        }

        #region base operations

        public bool Save(CFolder item, bool withCommit = true)
        {
            _repoFolder.Save(item);
            if (withCommit)
                ManagerSite.SaveDataBase();
            return true;
        }

        public bool Delete(CFolder item)
        {
            if (item == null)
                return false;
            _repoFolder.Delete(item);
            ManagerSite.SaveDataBase();
            return true;
        }

        #endregion

        #region queries

        public CFolder GetFolder(int id)
        {
            return _repoFolder.GetById(id);
        }

        public List<CFolder> GetFolders()
        {
            return _repoFolder.GetAll().ToList();
        }

        #endregion

        public CFolder NewFolder(string name)
        {
            return new CFolder(name);
        }

        public CFolder CreateFolder(CFolder folderPattern)
        {
            var folder = NewFolder(folderPattern.Name);
            Save(folder);
            return folder;
        }

        public CFolder EditFolder(CFolder folderPattern)
        {
            var folder = GetFolder(folderPattern.Id);
            if (folder != null)
            {
                folder.Name = folderPattern.Name;
                Save(folder);
            }
            return folder;
        }
        
        public FoldersVM CreateFoldersVM()
        {
            var folders = GetFolders();
            var vm = new FoldersVM();
            vm.Folders = Mapper.Map<List<FolderRowVM>>(folders);
            return vm;
        }
        
        public FolderEditVM CreateFolderEditVM()
        {
            var folder = NewFolder("");
            return CreateFolderEditVM(folder);
        }

        public FolderEditVM CreateFolderEditVM(CFolder folder)
        {
            var res = Mapper.Map<FolderEditVM>(folder);
            return res;
        }
    }
}