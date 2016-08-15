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
    public class TaskTest : PlanTests
    {
        [TestInitialize]
        public void Init()
        {
            CleanDB();
        }

        [TestMethod]
        public void Create()
        {
            var task = CreateTask();
            Assert.AreEqual(1, GetAllTasks().Count);
            var task2 = GetTask(task.Id);
            Assert.AreEqual(task.Name, task2.Name);
        }

        [TestMethod]
        public void Update()
        {
            var task = CreateTask();
            task.Name = "new task";
            SaveTask(task);
            var task2 = GetTask(task.Id);
            Assert.AreEqual(task.Name, task2.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var task = CreateTask();
            DeleteTasks(task);
            Assert.AreEqual(0, GetAllTasks().Count);
            Assert.AreEqual(null, GetTask(task.Id));
        }

        [TestMethod]
        public void StusesTask()
        {
            var task = CreateTask();
            Assert.AreEqual(null, task.DateClosed);
            Assert.AreEqual(CTaskStatus.Open, task.Status);

            mng.Tasks.CompleteTask(task);
            Assert.IsTrue(task.DateClosed != null);
            Assert.AreEqual(CTaskStatus.Completed, task.Status);

            task.Status = CTaskStatus.Open;
            SaveTask(task);
            Assert.AreEqual(null, task.DateClosed);
            Assert.AreEqual(CTaskStatus.Open, task.Status);

            task.Status = CTaskStatus.Canceled;
            SaveTask(task);
            Assert.IsTrue(task.DateClosed != null);
            Assert.AreEqual(CTaskStatus.Canceled, task.Status);
            DeleteTasks(task);
        }
    }
}
