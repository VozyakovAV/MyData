using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Domain.Memory;

namespace Domain
{
    public class FolderMap : EntityTypeConfiguration<CFolder>
    {
        public FolderMap()
        {
            ToTable("mem_folders");

            HasKey(x => x.Id);

            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name").IsRequired();
        }
    }
}