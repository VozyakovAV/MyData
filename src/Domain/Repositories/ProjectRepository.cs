using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Domain
{
    public class ProjectRepository : RepositoryBase<CProject>
    {
        public ProjectRepository(MyDbContext db, DbSet<CProject> collection) : base(db, collection)
        { }

        public override IQueryable<CProject> GetAll()
        {
            return base.GetAll().ExcludeDeleted();
        }

        public IQueryable<CProject> GetAllWithDeleted()
        {
            return base.GetAll();
        }
    }

    public static class QueriesProjects
    {
        public static IQueryable<CProject> ExcludeDeleted(this IQueryable<CProject> list)
        {
            return list.Where(x => !x.IsDeleted);
        }

        public static IQueryable<CProject> IncludeTasks(this IQueryable<CProject> list)
        {
            return list.Include(x => x.Tasks);
        }

        public static IQueryable<CProject> IncludeProjects(this IQueryable<CProject> list)
        {
            return list.Include("Projects.Projects.Projects.Projects.Projects.Projects.Projects");
        }
    }
}