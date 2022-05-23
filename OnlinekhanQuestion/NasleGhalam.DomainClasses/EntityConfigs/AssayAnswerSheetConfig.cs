using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AssayAnswerSheetConfig : EntityTypeConfiguration<AssayAnswerSheet>
    {
        public AssayAnswerSheetConfig()
        {
            HasKey(x => x.Id);
            

            HasRequired(x => x.User)
                .WithMany(x => x.AssayAnswerSheets)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

     

            HasRequired(x => x.Assay)
                .WithMany(x => x.AssayAnswerSheets)
                .HasForeignKey(x => x.AssayId)
                .WillCascadeOnDelete(false);

            
        }
    }
}
