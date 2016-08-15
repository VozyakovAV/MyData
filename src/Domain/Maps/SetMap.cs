using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Domain.Memory;

namespace Domain
{
    public class SetMap : EntityTypeConfiguration<CSet>
    {
        public SetMap()
        {
            ToTable("mem_sets");

            HasKey(x => x.Id);

            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name").IsRequired();
            Property(t => t.FolderID).HasColumnName("folderID");

            HasRequired(t => t.Folder)
                .WithMany(p => p.Sets)
                .HasForeignKey(t => t.FolderID)
                .WillCascadeOnDelete(true);
        }
    }
}