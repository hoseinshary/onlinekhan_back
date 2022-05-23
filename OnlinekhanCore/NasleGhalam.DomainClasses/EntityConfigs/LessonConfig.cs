using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class LessonConfig : EntityTypeConfiguration<Lesson>
    {
        public LessonConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(200).IsRequired();
            HasIndex(x => x.Name).IsUnique().HasName("UK_Lesson_Name");




            HasRequired(x => x.Lookup_Nezam)
              .WithMany(x => x.Lesson_Nezams)
              .HasForeignKey(x => x.LookupId_Nezam)
              .WillCascadeOnDelete(false);


            HasMany(x => x.LessonDepartments)
                .WithMany(x => x.Lessons)
                .Map(config =>
                {
                    config.MapLeftKey("LessonId");
                    config.MapRightKey("LessonDepartmentId");
                    config.ToTable("Lessons_LessonDepartments");
                });



        }
    }
}
