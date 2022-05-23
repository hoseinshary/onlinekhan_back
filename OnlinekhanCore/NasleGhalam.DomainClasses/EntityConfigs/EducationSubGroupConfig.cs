using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationSubGroupConfig : EntityTypeConfiguration<EducationSubGroup>
    {
        public EducationSubGroupConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();

            HasRequired(x => x.EducationTree)
                .WithMany(x => x.EducationSubGroups)
                .HasForeignKey(x => x.EducationTreeId)
                .WillCascadeOnDelete(false);
        }
    }
}
