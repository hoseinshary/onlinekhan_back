using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ControllerConfig : EntityTypeConfiguration<Controller>
    {
        public ControllerConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.FaName).HasMaxLength(50).IsRequired();
            Property(x => x.EnName).HasMaxLength(50).IsRequired();
            Property(x => x.Icone).HasMaxLength(200).IsRequired();

            HasRequired(x => x.Module)
                .WithMany(x => x.Controllers)
                .HasForeignKey(x => x.ModuleId)
                .WillCascadeOnDelete(false);
        }
    }
}
