using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Domain
{
    public class TaskMap : EntityTypeConfiguration<CTask>
    {
        public TaskMap()
        {
            ToTable("pln_tasks");

            HasKey(x => x.Id);

            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name").IsRequired();
            Property(t => t.Description).HasColumnName("description");
            Property(t => t.DateCreated).HasColumnName("dateCreated");
            Property(t => t.DateClosed).HasColumnName("dateClosed");
            Property(t => t.Deadline).HasColumnName("deadline");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.IsDeleted).HasColumnName("isDeleted");

            Property(t => t.ProjectID).HasColumnName("projectID");
            Property(t => t.IterationID).HasColumnName("iterationID");

            HasOptional(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID)
                .WillCascadeOnDelete(true);

            HasOptional(t => t.Iteration)
                .WithMany(w => w.Tasks)
                .HasForeignKey(t => t.IterationID);
        }
    }
}