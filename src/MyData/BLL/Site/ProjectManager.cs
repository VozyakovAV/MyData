using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyData.Models;
using System.Web.Mvc;
using AutoMapper;

namespace MyData.BLL.Site
{
    public class ProjectManager : ManagerBase
    {
        ProjectRepository _repo;

        public ProjectManager(ManagerSite manager, ProjectRepository repo) : base(manager)
        {
            this._repo = repo;
        }

        #region base operations

        public bool Save(CProject item, bool withCommit = true)
        {
            _repo.Save(item);
            if (withCommit)
                ManagerSite.SaveDataBase();
            return true;
        }

        public bool Delete(CProject project)
        {
            return Delete(project.Id);
        }

        public bool Delete(int id)
        {
            var project = GetProjectWithTree(id);
            if (project != null)
            {
                DeleteRecursively(project);
                ManagerSite.SaveDataBase();
                return true;
            }
            return false;
        }

        private void DeleteRecursively(CProject item)
        {
            item.IsDeleted = true;
            var tasks = ManagerSite.Tasks.GetTasksByProject(item.Id);
            foreach (var t in tasks)
            {
                t.IsDeleted = true;
                ManagerSite.Tasks.Save(t, false);
            }
            Save(item, false);

            foreach (var p in item.Projects)
                DeleteRecursively(p);
        }

        public bool CompleteDelete(CProject item, string confirm)
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

        public CProject GetProject(int id)
        {
            return _repo.GetById(id);
        }

        public CProject GetProjectByName(string name)
        {
            return _repo.GetAll().FirstOrDefault(x => x.Name == name);
        }

        public CProject GetProjectWithTree(int id)
        {
            var res = _repo.GetAll().IncludeProjects().SingleOrDefault(x => x.Id == id);
            if (res != null) ExcludeDeleted(res.Projects);
            return res;
        }

        public List<CProject> GetTreeProjects(int? parentID = null)
        {
            var res = _repo.GetAll().IncludeProjects().Where(x => x.ParentID == parentID).ToList();
            if (res != null) ExcludeDeleted(res);
            return res.ToList();
        }

        public List<CProject> GetTreeOpenProjects()
        {
            var res = _repo.GetAll().IncludeProjects().Where(x => x.ParentID == null && x.Status == CProjectStatus.Open).ToList();
            if (res != null) ExcludeDeleted(res);
            return res.ToList();
        }

        public List<CProject> GetProjects()
        {
            return _repo.GetAll().ToList();
        }

        private void ExcludeDeleted(ICollection<CProject> projects)
        {
            //TODO: сделать нормальную выборку из БД
            //https://www.daniweb.com/programming/software-development/tutorials/495287/selective-includes-with-entity-framework
            var excluded = projects.Where(x => x.IsDeleted).ToList();
            foreach (var ex in excluded)
                projects.Remove(ex);
            foreach (var pr in projects)
                ExcludeDeleted(pr.Projects);
        }

        private void ExcludeProject(ICollection<CProject> projects, int id)
        {
            var excluded = projects.Where(x => x.Id == id).ToList();
            foreach (var ex in excluded)
                projects.Remove(ex);
            foreach (var pr in projects)
                ExcludeProject(pr.Projects, id);
        }

        #endregion



        public CProject NewProject(string name)
        {
            return new CProject(name);
        }

        public CProject CreateProject(CProject projectPattern)
        {
            var project = NewProject(projectPattern.Name);
            project.Description = projectPattern.Description;
            project.ParentID = projectPattern.ParentID;
            project.Status = projectPattern.Status;
            project.Deadline = projectPattern.Deadline;
            Save(project);
            return project;
        }

        public CProject EditProject(CProject projectPattern)
        {
            var project = GetProject(projectPattern.Id);
            if (project != null)
            {
                project.Name = projectPattern.Name;
                project.Description = projectPattern.Description;
                project.Status = projectPattern.Status;
                project.Deadline = projectPattern.Deadline;

                if (projectPattern.ParentID == null)
                    project.ParentID = null;
                else
                {
                    var projectParent = GetProject(projectPattern.ParentID.Value);
                    if (projectParent != null)
                        project.ParentID = projectParent.Id; // TODO: может как то по другому можно?
                }
                Save(project);
            }
            return project;
        }

        public ProjectEditVM CreateProjectVM()
        {
            var project = NewProject("");
            return CreateProjectVM(project);
        }

        public ProjectEditVM CreateProjectVM(CProject project)
        {
            var res = new ProjectEditVM(project);

            var projects = GetTreeProjects();
            ExcludeProject(projects, project.Id);

            var list = CreateTreeProjectsForSelection(projects);
            res.Projects = new SelectList(list, "Id", "Name");
            return res;
        }

        public List<object> CreateTreeProjectsForSelection(List<CProject> projects)
        {
            var list = new List<object>();
            foreach (var pr in projects)
                GetTreeProjectSimple(list, pr, 0);
            return list;
        }

        private void GetTreeProjectSimple(List<object> listProjects, CProject project, int step = 0)
        {
            var name = (new String('–', step) + " " + project.Name).Trim();
            var obj = new { Name = name, Id = project.Id };
            listProjects.Add(obj);
            foreach (var pr in project.Projects)
                GetTreeProjectSimple(listProjects, pr, step + 1);
        }

        public ProjectVM CreateProjectVM(int projectID)
        {
            var project = GetProject(projectID);
            if (project == null)
                return null;

            var subProjects = GetTreeProjects(projectID);
            var tasks = ManagerSite.Tasks.GetTasksByProject(projectID);
            var openTasks = tasks.Where(x => x.Status == CTaskStatus.Open).OrderBy(x => x.DateCreated).ToList();
            var clodesTasks = tasks.Where(x => x.Status != CTaskStatus.Open).OrderByDescending(x => x.DateClosed).ToList();

            var vm = Mapper.Map<ProjectVM>(project);
            vm.SubProjects = Mapper.Map<List<ProjectRowVM>>(subProjects);
            vm.OpenTasks = Mapper.Map<List<TaskRowVM>>(openTasks);
            vm.ClosedTasks = Mapper.Map<List<TaskRowVM>>(clodesTasks);
            return vm;
        }

        public ProjectsVM GetProjectsVM()
        {
            var projects = GetTreeProjects();
            var vm = new ProjectsVM();
            var num = 1;
            GenerateTreeProjectsVM(vm, projects, ref num);
            return vm;
        }

        private void GenerateTreeProjectsVM(ProjectsVM vm, IEnumerable<CProject> projects, ref int number, int parentNumber = 0)
        {
            foreach (var project in projects)
            {
                var pr = new ProjectWithTreeVM()
                {
                    Project = project,
                    Number = number,
                    ParentNumber = parentNumber
                };
                vm.Projects.Add(pr);
                number++;
                GenerateTreeProjectsVM(vm, project.Projects, ref number, pr.Number);
            }
        }
    }
}