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
    public class SetTest : MemoryTests
    {
        [TestInitialize]
        public void Init()
        {
            CleanDB();
        }

        [TestMethod]
        public void Create()
        {
            var set = CreateSet();
            Assert.AreEqual(1, GetAllSets().Count);
            var set2 = GetSet(set.Id);
            Assert.AreEqual(set.Name, set2.Name);
        }

        [TestMethod]
        public void Update()
        {
            var set = CreateSet();
            set.Name = "new set";
            SaveSet(set);
            var set2 = GetSet(set.Id);
            Assert.AreEqual(set.Name, set2.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var set = CreateSet();
            DeleteSets(set);
            Assert.AreEqual(0, GetAllSets().Count);
            Assert.AreEqual(null, GetTerm(set.Id));
        }
    }
}
