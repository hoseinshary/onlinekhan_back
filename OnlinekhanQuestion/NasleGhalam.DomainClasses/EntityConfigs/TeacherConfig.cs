using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class TeacherConfig : EntityTypeConfiguration<Teacher>
    {
        public TeacherConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.FatherName).HasMaxLength(50);
            Property(x => x.Address).HasMaxLength(300);

        }
    }
}
