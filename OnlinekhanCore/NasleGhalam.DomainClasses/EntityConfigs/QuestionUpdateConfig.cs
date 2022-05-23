using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionUpdateConfig : EntityTypeConfiguration<QuestionUpdate>
    {
        public QuestionUpdateConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Description).IsMaxLength();
            Property(x => x.QuestionActivity).IsRequired();

            HasRequired(x => x.Question)
                .WithMany(x => x.QuestionUpdates)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.User)
                .WithMany(x => x.QuestionsUpdates)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

        }
    }
}
