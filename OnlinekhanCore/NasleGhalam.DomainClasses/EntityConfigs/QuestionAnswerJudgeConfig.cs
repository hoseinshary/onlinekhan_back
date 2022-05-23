using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionAnswerJudgeConfig : EntityTypeConfiguration<QuestionAnswerJudge>
    {
        public QuestionAnswerJudgeConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Description).HasMaxLength(400);

            HasRequired(x => x.QuestionAnswer)
                .WithMany(x => x.QuestionAnswerJudges)
                .HasForeignKey(x => x.QuestionAnswerId)
                .WillCascadeOnDelete(false);


            HasRequired(x => x.User)
                .WithMany(x => x.QuestionAnswerJudges)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

           

            HasRequired(x => x.Lookup_ReasonProblem)
                .WithMany(x => x.QuestionAnswerJudge_ReasonProblem)
                .HasForeignKey(x => x.LookupId_ReasonProblem)
                .WillCascadeOnDelete(false);

            
        }
    }
}
