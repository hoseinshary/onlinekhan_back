using System.Data.Entity.ModelConfiguration;
using Onlinekhan.SSO.DomainClasses.Entities;

namespace Onlinekhan.SSO.DomainClasses.EntityConfigs
{
    public class LogConfig : EntityTypeConfiguration<Log>
    {
        public LogConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.TableName).HasMaxLength(150).IsRequired();
            Property(x => x.CrudType).IsRequired();
            Property(x => x.DateTime).IsRequired();
            Property(x => x.ObjectId).IsRequired();
            Property(x => x.ObjectValue).HasMaxLength(200)
                .HasColumnType("nvarchar(max)").IsRequired();
           
        }
    }
}
