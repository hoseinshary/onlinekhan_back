using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
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
            HasRequired(x => x.User)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
