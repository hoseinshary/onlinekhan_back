using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.FatherName).HasMaxLength(50);
            Property(x => x.Address).HasMaxLength(300);


        }
    }
}
