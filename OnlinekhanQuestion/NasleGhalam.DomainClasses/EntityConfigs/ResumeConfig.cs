using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ResumeConfig : EntityTypeConfiguration<Resume>
    {
        public ResumeConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(150).IsRequired();
            Property(x => x.Family).HasMaxLength(150).IsRequired();
            Property(x => x.FatherName).HasMaxLength(150).IsRequired();
            Property(x => x.NationalNo).HasMaxLength(10);
            Property(x => x.IdNumber).HasMaxLength(10);
            Property(x => x.Phone).HasMaxLength(11).IsRequired();
            Property(x => x.Mobile).HasMaxLength(11).IsRequired();
            Property(x => x.Religion).HasMaxLength(50).IsRequired();
            Property(x => x.CityBorn).HasMaxLength(50).IsRequired();
            Property(x => x.Address).IsRequired().HasMaxLength(300);
            Property(x => x.PostCode).HasMaxLength(10).IsRequired();
            Property(x => x.FatherJob).HasMaxLength(150);
            Property(x => x.FatherPhone).HasMaxLength(11);
            Property(x => x.MotherJob).HasMaxLength(150);
            Property(x => x.MotherPhone).HasMaxLength(11);
            Property(x => x.PartnerJob).HasMaxLength(150);
            Property(x => x.PartnerPhone).HasMaxLength(11);
            Property(x => x.PhoneReagent1).HasMaxLength(11);
            Property(x => x.AddressReagent1).HasMaxLength(300);
            Property(x => x.PhoneReagent2).HasMaxLength(11);
            Property(x => x.AddressReagent2).HasMaxLength(300);
            Property(x => x.Description).HasMaxLength(300);
            Property(x => x.EducationCertificateJson).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.PublicationJson).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.TeachingResumeJson).IsRequired().HasColumnType("nvarchar(max)");

            HasRequired(x => x.City)
                .WithMany(x => x.Resumes)
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);
        }
    }
}