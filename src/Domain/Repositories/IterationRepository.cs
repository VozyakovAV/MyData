using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Domain
{
    public class IterationRepository : RepositoryBase<CIteration>
    {
        public IterationRepository(MyDbContext db, DbSet<CIteration> collection) : base(db, collection)
        { }

        public override IQueryable<CIteration> GetAll()
        {
            return base.GetAll().ExcludeDeleted();
        }

        public IQueryable<CIteration> GetAllWithDeleted()
        {
            return base.GetAll();
        }
    }

    public static class QueriesIterations
    {
        public static IQueryable<CIteration> ExcludeDeleted(this IQueryable<CIteration> list)
        {
            return list.Where(x => !x.IsDeleted);
        }

        public static IQueryable<CIteration> IncludeTasks(this IQueryable<CIteration> list)
        {
            return list.Include("Tasks.Project");
        }

    }
}