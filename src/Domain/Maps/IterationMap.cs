using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Domain
{
    public class IterationMap : EntityTypeConfiguration<CIteration>
    {
        public IterationMap()
        {
            ToTable("pln_iteration");

            HasKey(x => x.Id);

            Property(t => t.Id).HasColumnName("id");
            Property(t => t.DateStart).HasColumnName("dateStart");
            Property(t => t.DateFinish).HasColumnName("dateFinish");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.IsDeleted).HasColumnName("isDeleted");
        }
    }
}