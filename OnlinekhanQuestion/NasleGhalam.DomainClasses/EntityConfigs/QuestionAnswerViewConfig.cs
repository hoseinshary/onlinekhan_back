using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionAnswerViewConfig : EntityTypeConfiguration<QuestionAnswerView>
    {
        public QuestionAnswerViewConfig()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.Answer)
                .WithMany(x => x.QuestionAnswerViews)
                .HasForeignKey(x => x.AnswerId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Student)
                .WithMany(x => x.QuestionAnswerViews)
                .HasForeignKey(x => x.StudentId)
                .WillCascadeOnDelete(false);

        }
    }
}
