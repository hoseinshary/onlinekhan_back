using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AssaySchaduleConfig : EntityTypeConfiguration<AssaySchedule>
    {
        public AssaySchaduleConfig()
        {
            HasKey(x => x.Id);


            HasRequired(x => x.Assay)
                .WithMany(x => x.AssaySchedules)
                .HasForeignKey(x => x.AssayId)
                .WillCascadeOnDelete(false);





        }
    }
}
