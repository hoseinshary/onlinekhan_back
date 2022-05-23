using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionOptionConfig : EntityTypeConfiguration<QuestionOption>
    {
        public QuestionOptionConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Context).IsRequired().HasColumnType("nvarchar(max)");

            HasRequired(x => x.Question)
                .WithMany(x => x.QuestionOptions)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);
        }
    }
}
