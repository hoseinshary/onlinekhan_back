using System.Data.Entity.ModelConfiguration;
using Onlinekhan.SSO.DomainClasses.Entities;

namespace Onlinekhan.SSO.DomainClasses.EntityConfigs
{
    public class ProvinceConfig : EntityTypeConfiguration<Province>
    {
        public ProvinceConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            HasIndex(x => x.Name).IsUnique().HasName("UK_Province_Name");
            Property(x => x.Code).HasMaxLength(5).IsRequired();
        }
    }
}
