using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ErrorConfig : EntityTypeConfiguration<Error>
    {
        public ErrorConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.ErrorCode).IsRequired();
            Property(x => x.Ip).IsRequired();
            Property(x => x.Route).IsRequired();
        }
    }
}
