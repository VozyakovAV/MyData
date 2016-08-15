using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Domain
{
    public class ProjectMap : EntityTypeConfiguration<CProject>
    {
        public ProjectMap()
        {
            ToTable("pln_projects");

            HasKey(x => x.Id);

            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name").IsRequired();
            Property(t => t.Description).HasColumnName("description");
            Property(t => t.DateCreated).HasColumnName("dateCreated");
            Property(t => t.DateClosed).HasColumnName("dateClosed");
            Property(t => t.Deadline).HasColumnName("deadline");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.IsDeleted).HasColumnName("isDeleted");

            Property(t => t.ParentID).HasColumnName("parentID");

            HasOptional(p => p.Parent)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.ParentID)
                .WillCascadeOnDelete(false); // если поставить каскадность, то будет ошибка при создании базы
        }
    }
}