using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ProgramItemConfig : EntityTypeConfiguration<ProgramItem>
    {
        public ProgramItemConfig()
        {
            HasKey(x => x.Id);


            HasRequired(x => x.Program)
                .WithMany(x => x.ProgramItems)
                .HasForeignKey(x => x.ProgramId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_ProgramItemName)
                .WithMany(x => x.ProgramItem_Name)
                .HasForeignKey(x => x.LookupId_PrgoramItemName)
                .WillCascadeOnDelete(false);

        }
    }
}
