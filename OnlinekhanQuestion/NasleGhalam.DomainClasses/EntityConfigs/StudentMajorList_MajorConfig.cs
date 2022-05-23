using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;


namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class StudentMajorList_MajorConfig : EntityTypeConfiguration<StudentMajorList_Major>
    {
        public StudentMajorList_MajorConfig()
        {
            HasKey(x => x.Id);

            Property(x => x.Priority).IsRequired();
            HasRequired(x => x.Major)
          .WithMany(x => x.StudentMajorList_Major)
          .HasForeignKey(x => x.MajorsId)
          .WillCascadeOnDelete(false);

        }


    }
}
