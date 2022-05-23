using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionEqualConfig : EntityTypeConfiguration<QuestionEqual>
    {
        public QuestionEqualConfig()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.Question1)
                .WithMany(x => x.QuestionEquals1)
                .HasForeignKey(x => x.QuestionId1)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Question2)
                .WithMany(x => x.QuestionEquals2)
                .HasForeignKey(x => x.QuestionId2)
                .WillCascadeOnDelete(false);
        }
    }
}
