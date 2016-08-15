using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Memory;

namespace Domain
{
    public class SetRepository : RepositoryBase<CSet>
    {
        public SetRepository(MyDbContext db, DbSet<CSet> collection) : base(db, collection)
        { }
    }

    public static class QueriesSets
    {
    }
}