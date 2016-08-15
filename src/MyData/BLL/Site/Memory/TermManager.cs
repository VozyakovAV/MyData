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
        TermRepository _repoTerm;

        #region base operations

        public bool Save(CTerm item, bool withCommit = true)
        {
            _repoTerm.Save(item);
            if (withCommit)
                ManagerSite.SaveDataBase();
            return true;
        }

        public bool Delete(CTerm item)
        {
            _repoTerm.Delete(item);
            ManagerSite.SaveDataBase();
            return true;
        }

        #endregion

        #region queries

        public CTerm GetTerm(int id)
        {
            return _repoTerm.GetById(id);
        }

        public List<CTerm> GetTerms(int setID)
        {
            return _repoTerm.GetAll().Where(x => x.SetID == setID).ToList();
        }

        #endregion

        public CTerm NewTerm(string question)
        {
            return new CTerm(question);
        }

        public CTerm CreateTerm(CTerm termPattern)
        {
            var term = NewTerm(termPattern.Question);
            term.Question = termPattern.Question;
            term.Answer = termPattern.Answer;
            term.SetID = termPattern.SetID;
            Save(term);
            return term;
        }

        public CTerm EditTerm(CTerm termPattern)
        {
            var term = GetTerm(termPattern.Id);
            if (term != null)
            {
                term.Question = termPattern.Question;
                term.Answer = termPattern.Answer;
                term.SetID = termPattern.SetID;
                Save(term);
            }
            return term;
        }

        public TermsVM CreateTermsVM(int setID)
        {
            var terms = GetTerms(setID);
            var set = GetSet(setID);
            var vm = new TermsVM();
            vm.SetID = setID;
            vm.SetName = set.Name;
            vm.Terms = Mapper.Map<List<TermRowVM>>(terms);
            return vm;
        }

        public TermEditVM CreateTermEditVM(int setID = 0)
        {
            var term = NewTerm("");
            term.SetID = setID;
            return CreateTermEditVM(term);
        }

        public TermEditVM CreateTermEditVM(CTerm term)
        {
            var res = Mapper.Map<TermEditVM>(term);
            var set = GetSet(term.SetID);
            var sets = GetSets(set.FolderID);
            res.SetID = term.SetID;
            res.Sets = new SelectList(sets, "Id", "Name", term.Set);
            return res;
        }
    }
}