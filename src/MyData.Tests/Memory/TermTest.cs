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
    }
}
