using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Domain.Memory;

namespace Domain
{
    public class TermMap : EntityTypeConfiguration<CTerm>
    {
        public TermMap()
        {
            ToTable("mem_terms");

            HasKey(x => x.Id);

            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Question).HasColumnName("question").IsRequired();
            Property(t => t.Answer).HasColumnName("answer").IsRequired();
            Property(t => t.SetID).HasColumnName("setID");

            HasRequired(t => t.Set)
                .WithMany(p => p.Questions)
                .HasForeignKey(t => t.SetID)
                .WillCascadeOnDelete(true);
        }
    }
}