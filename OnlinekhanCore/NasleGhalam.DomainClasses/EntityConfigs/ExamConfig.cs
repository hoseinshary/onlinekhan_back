using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ExamConfig : EntityTypeConfiguration<Exam>
    {
        public ExamConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();

            HasRequired(x => x.EducationTree)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.EducationTreeId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.EducationYear)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.EducationYearId)
                .WillCascadeOnDelete(false);
        }
    }
}
