using System.Data.Entity.ModelConfiguration;
using Onlinekhan.SSO.DomainClasses.Entities;

namespace Onlinekhan.SSO.DomainClasses.EntityConfigs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Family).HasMaxLength(50).IsRequired();
            Property(x => x.Username).HasMaxLength(50).IsRequired();
            HasIndex(x => x.Username).IsUnique().HasName("UK_User_Username");
            Property(x => x.Password).HasMaxLength(50).IsRequired();
            Property(x => x.NationalNo).HasMaxLength(10);
            HasIndex(x => x.NationalNo).IsUnique().HasName("UK_User_NationalNo");
            Property(x => x.Phone).HasMaxLength(8);
            Property(x => x.Mobile).HasMaxLength(11);

          

            HasRequired(x => x.City)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);

            
        }
    }
}
