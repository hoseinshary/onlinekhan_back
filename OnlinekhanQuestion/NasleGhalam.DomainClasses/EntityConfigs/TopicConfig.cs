using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class TopicConfig : EntityTypeConfiguration<Topic>
    {
        public TopicConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(200).IsRequired();
            //this.HasIndex(x => x.Title).IsUnique().HasName("UK_Topic_Name");
            Property(x => x.ParentTopicId).IsOptional();

            HasRequired(x => x.Lesson)
            .WithMany(x => x.Topics)
            .HasForeignKey(x => x.LessonId)
            .WillCascadeOnDelete(false);

            HasOptional(x => x.ParentTopic)
                .WithMany(x => x.ChildrenTopic)
                .HasForeignKey(x => x.ParentTopicId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_HardnessType)
                .WithMany(x => x.Topic_Hardnesses)
                .HasForeignKey(x => x.LookupId_HardnessType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_AreaType)
                .WithMany(x => x.Topic_AreaTypes)
                .HasForeignKey(x => x.LookupId_AreaType)
                .WillCascadeOnDelete(false);

            HasMany(x => x.EducationBooks)
                .WithMany(x => x.Topics)
                .Map(config =>
                {
                    config.MapLeftKey("TopicId");
                    config.MapRightKey("EducationBookId");
                    config.ToTable("Topics_EducationBooks");
                });


            HasMany(x => x.Questions)
                .WithMany(x => x.Topics)
                .Map(config =>
                {
                    config.MapLeftKey("TopicId");
                    config.MapRightKey("QuestionId");
                    config.ToTable("Topics_Questions");
                });


            HasMany(x => x.Medias)
                .WithMany(x => x.Topics)
                .Map(config =>
                {
                    config.MapLeftKey("TopicId");
                    config.MapRightKey("MediaId");
                    config.ToTable("Topics_Medias");
                });
        }
    }
}
