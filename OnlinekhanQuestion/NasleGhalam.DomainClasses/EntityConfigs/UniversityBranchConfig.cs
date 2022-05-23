using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class UniversityBranchConfig : EntityTypeConfiguration<UniversityBranch>
    {
        public UniversityBranchConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            HasIndex(x => x.Name).IsUnique().HasName("UK_UniversityBranch_Name");

            HasRequired(x => x.EducationSubGroup)
                .WithMany(x => x.UniversityBranches)
                .HasForeignKey(x => x.EducationSubGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}
