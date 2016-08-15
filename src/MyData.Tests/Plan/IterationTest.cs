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
    public class IterationTest : PlanTests
    {
        [TestInitialize]
        public void Init()
        {
            CleanDB();
        }

        [TestMethod]
        public void Create()
        {
            var iteration = CreateIteration();
            Assert.AreEqual(1, GetAllIterations().Count);
            var iteration2 = GetIteration(iteration.Id);
            Assert.AreEqual(iteration.Name, iteration2.Name);
        }

        [TestMethod]
        public void Update()
        {
            var iteration = CreateIteration();
            iteration.DateFinish = iteration.DateFinish.AddDays(5);
            SaveIteration(iteration);
            var iteration2 = GetIteration(iteration.Id);
            Assert.AreEqual(iteration.DateFinish, iteration.DateFinish);
        }

        [TestMethod]
        public void Delete()
        {
            var iteration = CreateIteration();
            DeleteIterations(iteration);
            Assert.AreEqual(0, GetAllIterations().Count);
            Assert.AreEqual(null, GetTask(iteration.Id));
        }
    }
}
