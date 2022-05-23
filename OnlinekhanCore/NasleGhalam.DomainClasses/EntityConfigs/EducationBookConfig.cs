using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationBookConfig : EntityTypeConfiguration<EducationBook>
    {
        public EducationBookConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(200).IsRequired();
            HasIndex(x => x.Name).IsUnique().HasName("UK_EducationBook_Name");

            

            HasRequired(x => x.Lesson)
                .WithMany(x => x.EducationBooks)
                .HasForeignKey(x => x.LessonId)
                .WillCascadeOnDelete(false);
        }
    }
}
