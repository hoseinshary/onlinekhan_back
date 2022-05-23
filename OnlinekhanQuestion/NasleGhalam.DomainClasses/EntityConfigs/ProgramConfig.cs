using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ProgramConfig : EntityTypeConfiguration<Program>
    {
        public ProgramConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();

            HasRequired(x => x.Student)
                .WithMany(x => x.Programs)
                .HasForeignKey(x => x.StudentId)
                .WillCascadeOnDelete(false);
        }
    }
}
