using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionAnswerConfig : EntityTypeConfiguration<QuestionAnswer>
    {
        public QuestionAnswerConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.FilePath).HasMaxLength(200);
            Property(x => x.Context).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.Description).HasMaxLength(300);
            

            HasRequired(x => x.Question)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.User)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_AnswerType)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.LookupId_AnswerType)
                .WillCascadeOnDelete(false);

            HasRequired(x=>x.Writer)
                .WithMany(x=>x.QuestionAnswers)
                .HasForeignKey(x=>x.WriterId)
                .WillCascadeOnDelete(false);
        }
    }
}
