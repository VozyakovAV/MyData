using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyData.Models;
using Domain;
using Common;
using AutoMapper;

namespace MyData.BLL.Site
{
    public class IterationManager : ManagerBase
    {
        public const int DAYS_PER_ITERATION = 7;
        IterationRepository _repo;

        public IterationManager(ManagerSite manager, IterationRepository repo) : base(manager)
        {
            this._repo = repo;
        }

        #region base operations

        public bool Save(CIteration item, bool withCommit = true)
        {
            _repo.Save(item);
            if (withCommit)
                ManagerSite.SaveDataBase();
            return true;
        }

        public bool Delete(int id)
        {
            var res = false;
            var item = GetIteration(id);
            if (item != null)
                res = Delete(item);
            return res;
        }

        public bool Delete(CIteration item)
        {
            item.IsDeleted = true;
            ManagerSite.SaveDataBase();
            return true;
        }

        public bool CompleteDelete(CIteration item, string confirm)
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

        public CIteration GetIteration(int id)
        {
            return _repo.GetAll().IncludeTasks().SingleOrDefault(x => x.Id == id);
        }

        public CIteration GetCurrentIteration()
        {
            return _repo.GetAll().IncludeTasks().ToList().FirstOrDefault(x => x.DateStart <= DateTime.Now && x.DateFinish >= DateTime.Now);
        }

        public CIteration GetLastIteration()
        {
            return _repo.GetAll().IncludeTasks().OrderByDescending(x => x.DateStart).FirstOrDefault();
        }

        public List<CIteration> GetIterations()
        {
            return _repo.GetAll().ToList();
        }

        public List<CIteration> GetOpenIterations()
        {
            return _repo.GetAll().Where(x => x.Status == CIterationStatus.Open).ToList();
        }

        #endregion

        public CIteration NewIteration()
        {
            var lastIteration = GetLastIteration();
            var dateLastIteration = lastIteration == null ? DateTime.Now.Date : lastIteration.DateFinish;
            var dateStart = dateLastIteration.GetNextMonday();
            return NewIteration(dateStart);
        }

        public CIteration NewIteration(DateTime start)
        {
            return new CIteration(start.Date, start.Date.AddDays(DAYS_PER_ITERATION - 1));
        }

        public CIteration CreateIteration(CIteration iterationPattern)
        {
            var iteration = NewIteration(iterationPattern.DateStart);
            iteration.Status = iterationPattern.Status;
            Save(iteration);
            return iteration;
        }

        public CIteration EditIteration(CIteration iterationPattern)
        {
            var iteration = GetIteration(iterationPattern.Id);
            if (iteration != null)
            {
                iteration.DateStart = iterationPattern.DateStart;
                iteration.DateFinish = iteration.DateStart.Date.AddDays(DAYS_PER_ITERATION - 1);
                iteration.Status = iterationPattern.Status;
                Save(iteration);
            }
            return iteration;
        }

        public IterationEditVM CreateIterationVM()
        {
            var Iteration = NewIteration();
            return CreateIterationVM(Iteration);
        }

        public IterationEditVM CreateIterationVM(CIteration iteration)
        {
            var res = new IterationEditVM(iteration);
            return res;
        }

        public IterationVM CreateIterationVM(int id)
        {
            var iteration = GetIteration(id);
            if (iteration == null)
                return null;

            var tasks = ManagerSite.Tasks.GetTasksByIteration(id).ToList();
            var openTasks = tasks.Opened().OrderBy(x => x.DateCreated).ToList();
            var closedTasks = tasks.Closed().OrderByDescending(x => x.DateClosed).ToList();

            var vm = Mapper.Map<IterationVM>(iteration);
            vm.OpenTasks = Mapper.Map<List<TaskRowVM>>(openTasks);
            vm.ClosedTasks = Mapper.Map<List<TaskRowVM>>(closedTasks);
            return vm;
        }

        public IterationsVM CreateIterationsVM()
        {
            var iterations = GetIterations();
            var vm = new IterationsVM();
            vm.Iterations = Mapper.Map<List<IterationRowVM>>(iterations);
            return vm;
        }
    }
}