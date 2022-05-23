using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class SaleConfig : EntityTypeConfiguration<Sale>
    {
        public SaleConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); ;

            HasRequired(x => x.User)
           .WithMany(x => x.Sales)
           .HasForeignKey(x => x.UserId)
           .WillCascadeOnDelete(false);

        }
    }
}
