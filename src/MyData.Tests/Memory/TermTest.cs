using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using MyData.BLL.Site;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MyData.Tests.Domains
{
    [TestClass]
    public class TermTest : MemoryTests
    {
        [TestInitialize]
        public void Init()
        {
            CleanDB();
        }

        [TestMethod]
        public void Create()
        {
            var term = CreateTerm();
            Assert.AreEqual(1, GetAllTerms(term.SetID).Count);
            var term2 = GetTerm(term.Id);
            Assert.AreEqual(term.Question, term2.Question);
        }

        [TestMethod]
        public void Update()
        {
            var term = CreateTerm();
            term.Question = "new question";
            SaveTerm(term);
            var term2 = GetTerm(term.Id);
            Assert.AreEqual(term.Question, term2.Question);
        }

        [TestMethod]
        public void Delete()
        {
            var term = CreateTerm();
            DeleteTerms(term);
            Assert.AreEqual(0, GetAllTerms(term.SetID).Count);
            Assert.AreEqual(null, GetTerm(term.Id));
        }

        [TestMethod]
        public void ImportTerms()
        {
            var set = CreateSet();
            Assert.AreEqual(0, GetAllTerms(set.Id).Count);
            var vm = mng.Memory.CreateImportVM(set.Id);
            vm.WordDelimeter = " ---- ";
            vm.RowDelimeter = " ;;;; ";
            vm.Text = "1 ---- 2 ;;;; 3 ---- 4";
            mng.ImportExportTerms.ImportTerms(vm.SetID, vm.Text, vm.WordDelimeter, vm.RowDelimeter);
            var terms = GetAllTerms(set.Id);
            Assert.AreEqual(2, terms.Count);
            Assert.AreEqual("1", terms[0].Question);
            Assert.AreEqual("2", terms[0].Answer);
            Assert.AreEqual("3", terms[1].Question);
            Assert.AreEqual("4", terms[1].Answer);
        }

        [TestMethod]
        public void ExportTerms()
        {
            var set = CreateSet();
            CreateTerm("1", "2", set);
            CreateTerm("3", "4", set);

            var vm = mng.Memory.CreateExportVM(set.Id);
            vm.WordDelimeter = " ---- ";
            vm.RowDelimeter = " ;;;; ";
            var text = mng.ImportExportTerms.ExportTerms(vm.SetID, vm.WordDelimeter, vm.RowDelimeter);
            Assert.AreEqual("1 ---- 2 ;;;; 3 ---- 4 ;;;; ", text.Replace(Environment.NewLine, ""));
        }
    }
}
