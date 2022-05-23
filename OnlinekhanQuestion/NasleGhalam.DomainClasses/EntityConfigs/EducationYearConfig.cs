using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationYearConfig : EntityTypeConfiguration<EducationYear>
    {
        public EducationYearConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            HasIndex(x => x.Name).IsUnique().HasName("UK_EducationYear_Name");
        }
    }
}
