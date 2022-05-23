using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class WriterConfig : EntityTypeConfiguration<Writer>
    {
        public WriterConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(150).IsRequired();

            HasOptional(x => x.User)
                .WithOptionalDependent(x => x.Writer);
        }
    }
}
