using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Memory;

namespace Domain
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDbContext") { }

        public DbSet<CProject> Projects { get; set; }
        public DbSet<CTask> Tasks { get; set; }
        public DbSet<CIteration> Iterations { get; set; }

        public DbSet<CSet> Sets { get; set; }
        public DbSet<CTerm> Questions { get; set; }
        public DbSet<CFolder> Folders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new IterationMap());

            modelBuilder.Configurations.Add(new FolderMap());
            modelBuilder.Configurations.Add(new SetMap());
            modelBuilder.Configurations.Add(new TermMap());
        }
    }
}