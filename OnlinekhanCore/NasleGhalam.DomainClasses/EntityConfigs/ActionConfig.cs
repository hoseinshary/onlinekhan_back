using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ActionConfig : EntityTypeConfiguration<Action>
    {
        public ActionConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.FaName).HasMaxLength(50).IsRequired();

            HasRequired(x => x.Controller)
                .WithMany(x => x.Actions)
                .HasForeignKey(x => x.ControllerId)
                .WillCascadeOnDelete(false);
        }
    }
}
