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
    public class FolderTest : MemoryTests
    {
        [TestInitialize]
        public void Init()
        {
            CleanDB();
        }

        [TestMethod]
        public void Create()
        {
            var folder = CreateFolder();
            Assert.AreEqual(1, GetAllFolders().Count);
            var folder2 = GetFolder(folder.Id);
            Assert.AreEqual(folder.Name, folder2.Name);
        }

        [TestMethod]
        public void Update()
        {
            var folder = CreateFolder();
            folder.Name = "new folder";
            SaveFolder(folder);
            var folder2 = GetFolder(folder.Id);
            Assert.AreEqual(folder.Name, folder2.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var folder = CreateFolder();
            DeleteFolders(folder);
            Assert.AreEqual(0, GetAllFolders().Count);
            Assert.AreEqual(null, GetFolder(folder.Id));
        }
    }
}
