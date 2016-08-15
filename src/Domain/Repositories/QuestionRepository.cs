using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Memory;

namespace Domain
{
    public class TermRepository : RepositoryBase<CTerm>
    {
        public TermRepository(MyDbContext db, DbSet<CTerm> collection) : base(db, collection)
        { }
    }

}