using NasleGhalam.DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class StudentMajorlistConfig : EntityTypeConfiguration<StudentMajorlist>
    {
        public StudentMajorlistConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(200).IsRequired();
            Property(x => x.CreationDate).IsRequired();
            HasRequired(x => x.Student)
            .WithMany(x => x.StudentMajorlists)
            .HasForeignKey(x => x.StudentId)
            .WillCascadeOnDelete(false);
            HasMany(x => x.Majors)
               .WithMany(x => x.StudentMajorlists)
               .Map(config =>
               {
                   config.MapLeftKey("StudentMajorlistId");
                   config.MapRightKey("MajorsId");
                   config.ToTable("StudentMajorlist_Majors");
               });

        }
    }
}
