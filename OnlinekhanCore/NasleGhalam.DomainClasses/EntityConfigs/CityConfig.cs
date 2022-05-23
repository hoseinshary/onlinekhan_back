using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class CityConfig : EntityTypeConfiguration<City>
    {
        public CityConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();

            HasRequired(x => x.Province)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.ProvinceId)
                .WillCascadeOnDelete(false);
        }
    }
}
