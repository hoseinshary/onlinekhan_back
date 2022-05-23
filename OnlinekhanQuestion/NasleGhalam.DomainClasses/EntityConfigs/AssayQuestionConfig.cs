using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AssayQuestionConfig : EntityTypeConfiguration<AssayQuestion>
    {
        public AssayQuestionConfig()
        {
            HasKey(x => x.Id);
            

            HasRequired(x => x.Question)
                .WithMany(x => x.AssayQuestions)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Assay)
                .WithMany(x => x.AssayQuestions)
                .HasForeignKey(x => x.AssayId)
                .WillCascadeOnDelete(false);

        

        }
    }
}
