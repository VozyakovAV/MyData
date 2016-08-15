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
    public class ProjectTest : PlanTests
    {
        [TestInitialize]
        public void Init()
        {
            CleanDB();
        }

        [TestMethod]
        public void Create()
        {
            var project = CreateProject();
            Assert.AreEqual(1, GetAllProjects().Count);
            var project2 = GetProject(project.Id);
            Assert.AreEqual(project.Name, project2.Name);
        }

        [TestMethod]
        public void Update()
        {
            var project = CreateProject();
            project.Name = "new project";
            SaveProject(project);
            var project2 = GetProject(project.Id);
            Assert.AreEqual(project.Name, project2.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var project = CreateProject();
            DeleteProjects(project);
            Assert.AreEqual(0, GetAllProjects().Count);
            Assert.AreEqual(null, GetTask(project.Id));
        }


        [TestMethod]
        public void CreateTasks()
        {
            var task1 = NewTask();
            var task2 = NewTask();
            var task3 = NewTask();

            var project = NewProject();
            AddTasksInProject(project, task1, task2, task3);

            SaveProject(project);
            Assert.AreEqual(3, GetAllTasks().Count);
            Assert.AreEqual(1, GetAllProjects().Count);

            DeleteProjects(project);
            Assert.AreEqual(0, GetAllTasks().Count);
            Assert.AreEqual(0, GetAllProjects().Count);
        }

        [TestMethod]
        public void DeleteProjectRoot()
        {
            CreateTreeProjects();

            Assert.AreEqual(3, GetAllProjects().Count);
            Assert.AreEqual(3, GetAllTasks().Count);
            FlushDB();

            var project = GetProject("project 1");
            DeleteProjects(project);

            FlushDB();
            Assert.AreEqual(0, GetAllProjects().Count);
            Assert.AreEqual(0, GetAllTasks().Count);
        }

        [TestMethod]
        public void DeleteProjectMiddle()
        {
            CreateTreeProjects();

            Assert.AreEqual(3, GetAllProjects().Count);
            Assert.AreEqual(3, GetAllTasks().Count);
            FlushDB();

            var project = GetProject("project 2");
            DeleteProjects(project);
            FlushDB();

            Assert.AreEqual(1, GetAllProjects().Count);
            Assert.AreEqual(1, GetAllTasks().Count);
        }

        [TestMethod]
        public void DeleteProjectLast()
        {
            CreateTreeProjects();

            Assert.AreEqual(3, GetAllProjects().Count);
            Assert.AreEqual(3, GetAllTasks().Count);
            FlushDB();

            var project = GetProject("project 3");
            DeleteProjects(project);
            FlushDB();

            Assert.AreEqual(2, GetAllProjects().Count);
            Assert.AreEqual(2, GetAllTasks().Count);
        }

        private void CreateTreeProjects()
        {
            var project = NewProject("project 1");
            var project_sub = NewProject("project 2");
            var project_sub_sub = NewProject("project 3");
            AddProjectsInProject(project, project_sub);
            AddProjectsInProject(project_sub, project_sub_sub);

            project.Tasks.Add(NewTask());
            project_sub.Tasks.Add(NewTask());
            project_sub_sub.Tasks.Add(NewTask());
            SaveProject(project);
        }
    }
}
