using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class HistoryEducationConfig : EntityTypeConfiguration<HistoryEducation>
    {
        public HistoryEducationConfig()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.Student)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.StudentId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Exam)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.ExamId)
                .WillCascadeOnDelete(false);

            

            
            HasRequired(x => x.EducationTree)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.EducationTreeId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Cities)
             .WithMany(x => x.HistoryEducations)
             .Map(config =>
             {
                 config.MapLeftKey("HistoryEducationId");
                 config.MapRightKey("CityId");
                 config.ToTable("HistoryEducations_Cities");
             });

            HasMany(x => x.UniversityBranches)
             .WithMany(x => x.HistoryEducations)
             .Map(config =>
             {
                 config.MapLeftKey("HistoryEducationId");
                 config.MapRightKey("UniversityBranchId");
                 config.ToTable("HistoryEducations_UniversityBranchs");
             });



        }
    }
}
