using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Memory;

namespace Domain
{
    public class FolderRepository : RepositoryBase<CFolder>
    {
        public FolderRepository(MyDbContext db, DbSet<CFolder> collection) : base(db, collection)
        { }
    }
}