using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class LookupConfig : EntityTypeConfiguration<Lookup>
    {
        public LookupConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Value).HasMaxLength(50).IsRequired();
        }
    }
}