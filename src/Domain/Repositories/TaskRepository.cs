using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Domain
{
    public class TaskRepository : RepositoryBase<CTask>
    {
        public TaskRepository(MyDbContext db, DbSet<CTask> collection) : base(db, collection)
        { }

        public override IQueryable<CTask> GetAll()
        {
            return base.GetAll().IncludeProjects().IncludeIterations().ExcludeDeleted()
                .OrderBy(x => x.Status == CTaskStatus.Completed)
                .ThenBy(x => x.DateClosed)
                .ThenBy(x => x.DateCreated);
        }

        public IQueryable<CTask> GetAllWithDeleted()
        {
            return base.GetAll().IncludeProjects().IncludeIterations();
        }
    }

    public static class QueriesTasks
    {
        public static IQueryable<CTask> ExcludeDeleted(this IQueryable<CTask> list)
        {
            return list.Where(x => !x.IsDeleted);
        }

        public static IQueryable<CTask> IncludeProjects(this IQueryable<CTask> list)
        {
            return list.Include(x => x.Project);
        }

        public static IQueryable<CTask> IncludeIterations(this IQueryable<CTask> list)
        {
            return list.Include(x => x.Iteration);
        }

        public static IEnumerable<CTask> Opened(this IEnumerable<CTask> list)
        {
            return list.Where(x => x.Status == CTaskStatus.Open);
        }
        public static IEnumerable<CTask> Closed(this IEnumerable<CTask> list)
        {
            return list.Where(x => x.Status != CTaskStatus.Open);
        }
    }
}