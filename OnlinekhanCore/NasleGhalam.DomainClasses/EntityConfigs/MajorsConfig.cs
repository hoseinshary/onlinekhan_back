using NasleGhalam.DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class MajorsConfig : EntityTypeConfiguration<Majors>
    {
        public MajorsConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Course).IsRequired();
            Property(x => x.Code).IsRequired();
            Property(x => x.MajorTitle).HasMaxLength(200)
                .HasColumnType("nvarchar(max)").IsRequired();
            Property(x => x.AdmissionFirst).IsRequired();
            Property(x => x.AdmissionSecond).IsRequired();
            Property(x => x.Woman).IsRequired();
            Property(x => x.Man).IsRequired();
            Property(x => x.Province).IsRequired();
            Property(x => x.Description).IsRequired();
            Property(x => x.Apply).IsRequired();
            Property(x => x.University).HasMaxLength(200).HasColumnType("nvarchar(max)").IsRequired();
            Property(x => x.Field).IsRequired();
                
        }
    }
}
