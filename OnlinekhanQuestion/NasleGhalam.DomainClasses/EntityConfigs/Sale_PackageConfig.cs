using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;


namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class Sale_PackageConfig : EntityTypeConfiguration<Sale_Package>
    {
        public Sale_PackageConfig()
        {
            HasKey(x => x.Id);


            HasRequired(x => x.Package)
          .WithMany(x => x.Sales_Packages)
          .HasForeignKey(x => x.PackageId)
          .WillCascadeOnDelete(false);

            HasRequired(x => x.Sale)
          .WithMany(x => x.Sale_Packages)
          .HasForeignKey(x => x.SaleId)
          .WillCascadeOnDelete(false);

        }


    }
}
