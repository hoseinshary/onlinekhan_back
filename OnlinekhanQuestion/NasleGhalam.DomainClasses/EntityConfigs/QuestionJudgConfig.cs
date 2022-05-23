using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionJudgConfig : EntityTypeConfiguration<QuestionJudge>
    {
        public QuestionJudgConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Description).HasMaxLength(400);

            HasRequired(x => x.Question)
                .WithMany(x => x.QuestionJudges)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);


            HasRequired(x => x.User)
                .WithMany(x => x.QuestionJudges)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);


            HasRequired(x => x.Lookup_QuestionHardnessType)
                .WithMany(x => x.QuestionJudge_QuestionHardnessTypes)
                .HasForeignKey(x => x.LookupId_QuestionHardnessType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_RepeatnessType)
                .WithMany(x => x.QuestionJudge_RepeatnessTypes)
                .HasForeignKey(x => x.LookupId_RepeatnessType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_WhereProblem)
                .WithMany(x => x.QuestionJudge_WhereProblem)
                .HasForeignKey(x => x.LookupId_WhereProblem)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_ReasonProblem)
                .WithMany(x => x.QuestionJudge_ReasonProblem)
                .HasForeignKey(x => x.LookupId_ReasonProblem)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_QuestionRank)
                .WithMany(x => x.QuestionJudge_QuestionRanks)
                .HasForeignKey(x => x.LookupId_QuestionRank)
                .WillCascadeOnDelete(false);
        }
    }
}
