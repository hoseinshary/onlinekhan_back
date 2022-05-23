using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class RatioConfig : EntityTypeConfiguration<Ratio>
    {
        public RatioConfig()
        {
            HasKey(x => x.Id);
            

            HasRequired(x => x.EducationSubGroup)
                .WithMany(x => x.Ratios)
                .HasForeignKey(x => x.EducationSubGroupId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lesson)
                .WithMany(x => x.Ratios)
                .HasForeignKey(x => x.LessonId)
                .WillCascadeOnDelete(false);
        }
    }
}
