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
    public class CommonTest : BaseTest
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void DeleteDBTest()
        {
            DeleteDB();
        }
    }
}
