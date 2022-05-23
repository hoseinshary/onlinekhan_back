using NasleGhalam.DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class TeacherGroupConfig : EntityTypeConfiguration<TeacherGroup>
    {
        public TeacherGroupConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired();
            HasRequired(x => x.Teacher)
            .WithMany(x => x.TeacherGroups)
            .HasForeignKey(x => x.TeacherId)
            .WillCascadeOnDelete(false);
            HasMany(x => x.Students)
               .WithMany(x => x.TeacherGroups)
               .Map(config =>
               {
                   config.MapLeftKey("TeacherGroupId");
                   config.MapRightKey("StudentId");
                   config.ToTable("TeacherGroup_Students");
               });

        }
    }
}
