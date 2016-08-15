using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using MyData.BLL.Site;

namespace MyData.Tests
{
    public class PlanTests : BaseTest
    {
        public override void CleanDB()
        {
            FlushDB();
            CleanTasks();
            CleanIterations();
            CleanProjects();
            FlushDB();
        }

        public void CleanTasks()
        {
            var tasks = mng.Tasks.GetTasks();
            foreach (var task in tasks)
                mng.Tasks.CompleteDelete(task, "delete");
        }

        public void CleanIterations()
        {
            var iterations = mng.Iterations.GetIterations();
            foreach (var iteration in iterations)
                mng.Iterations.CompleteDelete(iteration, "delete");
        }

        public void CleanProjects()
        {
            var projects = mng.Projects.GetProjects();
            foreach (var project in projects)
                mng.Projects.CompleteDelete(project, "delete");
        }

        #region tasks

        public CTask CreateTask(string name = "Задача")
        {
            var task = NewTask(name);
            mng.Tasks.Save(task);
            return task;
        }

        public CTask NewTask(string name = "Задача")
        {
            return mng.Tasks.NewTask(name);
        }

        public CTask GetTask(int id)
        {
            return mng.Tasks.GetTask(id);
        }

        public IList<CTask> GetAllTasks()
        {
            return mng.Tasks.GetTasks();
        }

        public void SaveTask(CTask task)
        {
            mng.Tasks.Save(task);
        }

        public bool DeleteTask(int id)
        {
            return mng.Tasks.Delete(id);
        }

        public bool DeleteTasks(IEnumerable<CTask> tasks)
        {
            return DeleteTasks(tasks.ToArray());
        }

        public bool DeleteTasks(params CTask[] tasks)
        {
            foreach (var task in tasks)
                mng.Tasks.Delete(task);
            return true;
        }

        public void DeleteAllTasks()
        {
            DeleteTasks(mng.Tasks.GetTasks());
        }

        #endregion

        #region iterations

        public CIteration CreateIteration()
        {
            var iteration = NewIteration();
            mng.Iterations.Save(iteration);
            return iteration;
        }

        public CIteration NewIteration()
        {
            return mng.Iterations.NewIteration(DateTime.Now);
        }

        public CIteration GetIteration(int id)
        {
            return mng.Iterations.GetIteration(id);
        }

        public IList<CIteration> GetAllIterations()
        {
            return mng.Iterations.GetIterations();
        }

        public void SaveIteration(CIteration iteration)
        {
            mng.Iterations.Save(iteration);
        }

        public bool DeleteIteration(int id)
        {
            return mng.Iterations.Delete(id);
        }

        public bool DeleteIterations(IEnumerable<CIteration> iterations)
        {
            return DeleteIterations(iterations.ToArray());
        }

        public bool DeleteIterations(params CIteration[] iterations)
        {
            foreach (var iteration in iterations)
                mng.Iterations.Delete(iteration);
            return true;
        }

        public void DeleteAllIterations()
        {
            DeleteIterations(mng.Iterations.GetIterations());
        }

        #endregion

        #region projects

        public CProject CreateProject(string name = "Проект")
        {
            var project = NewProject();
            mng.Projects.Save(project);
            return project;
        }

        public CProject NewProject(string name = "Проект")
        {
            return mng.Projects.NewProject(name);
        }

        public CProject GetProject(int id)
        {
            return mng.Projects.GetProject(id);
        }

        public CProject GetProject(string name)
        {
            return mng.Projects.GetProjectByName(name);
        }

        public IList<CProject> GetAllProjects()
        {
            return mng.Projects.GetProjects();
        }

        public void SaveProject(CProject project)
        {
            mng.Projects.Save(project);
        }

        public bool DeleteProject(int id)
        {
            return mng.Projects.Delete(id);
        }

        public bool DeleteProjects(IEnumerable<CProject> projects)
        {
            return DeleteProjects(projects.ToArray());
        }

        public bool DeleteProjects(params CProject[] projects)
        {
            foreach (var project in projects)
                mng.Projects.Delete(project);
            return true;
        }

        public void DeleteAllProjects()
        {
            DeleteProjects(mng.Projects.GetProjects());
        }

        public void AddTasksInProject(CProject project, params CTask[] tasks)
        {
            foreach (var task in tasks)
                project.Tasks.Add(task);
        }

        public void AddProjectsInProject(CProject project, params CProject[] projects)
        {
            foreach (var pr in projects)
                project.Projects.Add(pr);
        }

        #endregion
    }
}
