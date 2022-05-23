using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class LessonDepartmentConfig : EntityTypeConfiguration<LessonDepartment>
    {
        public LessonDepartmentConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            HasIndex(x => x.Name).IsUnique().HasName("UK_LessonDepartment_Name");



        }
    }
}
