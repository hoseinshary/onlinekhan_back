using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class BoxConfig : EntityTypeConfiguration<Box>
    {
        public BoxConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();

            HasRequired(x => x.Teacher)
                .WithMany(x => x.Boxes)
                .HasForeignKey(x => x.TeacherId)
                .WillCascadeOnDelete(false);
        }
    }
}
