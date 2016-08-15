using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MyData.Models;
using Domain;

namespace MyData.BLL.Site
{
    public class TaskManager : ManagerBase
    {
        TaskRepository _repo;

        public TaskManager(ManagerSite manager, TaskRepository repo) : base(manager)
        {
            this._repo = repo;
        }

        #region base operations

        public bool Save(CTask item, bool withCommit = true)
        {
            if (item.Status == CTaskStatus.Open)
                item.DateClosed = null;
            else if (item.DateClosed == null)
                item.DateClosed = DateTime.Now;

            _repo.Save(item);
            if (withCommit)
                ManagerSite.SaveDataBase();
            return true;
        }

        public bool Delete(int id)
        {
            var res = false;
            var item = GetTask(id);
            if (item != null)
                res = Delete(item);
            return res;
        }

        public bool Delete(CTask item)
        {
            item.IsDeleted = true;
            ManagerSite.SaveDataBase();
            return true;
        }

        public bool CompleteDelete(CTask item, string confirm)
        {
            if (confirm == "delete")
            {
                var res = Delete(item);
                ManagerSite.SaveDataBase();
                return res;
            }
            return false;
        }

        #endregion

        #region queries

        public CTask GetTask(int id)
        {
            return _repo.GetById(id);
        }

        public List<CTask> GetTasks()
        {
            return _repo.GetAll().ToList();
        }

        public List<CTask> GetTasksByIteration(int id)
        {
            return _repo.GetAll().Where(x => x.IterationID == id).ToList();
        }

        public List<CTask> GetTasksByProject(int id)
        {
            return _repo.GetAll().Where(x => x.ProjectID == id).ToList();
        }

        #endregion

        public CTask NewTask(string name)
        {
            return new CTask(name);
        }

        public CTask CreateTask(CTask taskPattern)
        {
            var task = NewTask(taskPattern.Name);
            task.Description = taskPattern.Description;
            task.ProjectID = taskPattern.ProjectID;
            task.IterationID = taskPattern.IterationID;
            task.Deadline = taskPattern.Deadline;
            task.Status = taskPattern.Status;
            Save(task);
            return task;
        }

        public CTask EditTask(CTask taskPattern)
        {
            var task = GetTask(taskPattern.Id);
            if (task != null)
            {
                task.Name = taskPattern.Name;
                task.Description = taskPattern.Description;
                task.Deadline = taskPattern.Deadline;
                task.Status = taskPattern.Status;
                var project = ManagerSite.Projects.GetProject(taskPattern.ProjectID.Value);
                if (project != null)
                    task.ProjectID = project.Id; // TODO: может как то по другому можно?

                if (!taskPattern.IterationID.HasValue)
                    task.IterationID = null;
                else
                {
                    var iteration = ManagerSite.Iterations.GetIteration(taskPattern.IterationID.Value);
                    if (iteration != null)
                        task.IterationID = iteration.Id; // TODO: может как то по другому можно?
                }
                Save(task);
            }
            return task;
        }

        public CTask CompleteTask(CTask taskPattern)
        {
            var task = GetTask(taskPattern.Id);
            if (task != null)
            {
                task.Status = CTaskStatus.Completed;
                Save(task);
            }
            return task;
        }

        public TaskEditVM CreateTaskVM(int? projectID)
        {
            var task = NewTask("");
            if (projectID.HasValue)
            {
                var project = ManagerSite.Projects.GetProject(projectID.Value);
                if (project != null)
                    task.AddProject(project);
            }
            return CreateTaskVM(task);
        }

        public TaskEditVM CreateTaskVM(CTask task)
        {
            var res = new TaskEditVM(task);
            var projects = ManagerSite.Projects.GetTreeProjects();
            var list = ManagerSite.Projects.CreateTreeProjectsForSelection(projects);
            res.Projects = new SelectList(list, "Id", "Name");

            var iterations = ManagerSite.Iterations.GetOpenIterations();
            if (task.IterationID.HasValue && !iterations.Any(x => x.Id == task.IterationID))
            {
                var currentItetation = ManagerSite.Iterations.GetIteration(task.IterationID.Value);
                if (currentItetation != null)
                    iterations.Add(currentItetation);
            }
            res.Iterations = new SelectList(iterations, "Id", "Name");

            return res;
        }
    }
}