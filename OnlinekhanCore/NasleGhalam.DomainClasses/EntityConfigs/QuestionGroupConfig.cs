using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionGroupConfig : EntityTypeConfiguration<QuestionGroup>
    {
        public QuestionGroupConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(200).IsRequired();
            
            

            HasRequired(x => x.Lesson)
            .WithMany(x => x.QuestionGroups)
            .HasForeignKey(x => x.LessonId)
            .WillCascadeOnDelete(false);

            HasRequired(x => x.User)
            .WithMany(x => x.QuestionGroups)
            .HasForeignKey(x => x.UserId)
            .WillCascadeOnDelete(false);


            HasMany(x => x.Questions)
                .WithMany(x => x.QuestionGroups)
                .Map(config =>
                {
                    config.MapLeftKey("QuestionGroupId");
                    config.MapRightKey("QuestionId");
                    config.ToTable("QuestionGroups_Questions");
                });

        }
    }
}
