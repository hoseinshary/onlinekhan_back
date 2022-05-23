using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class MediaConfig : EntityTypeConfiguration<Media>
    {
        public MediaConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).IsRequired().HasColumnType("nvarchar(max)");

            Property(x => x.Description).HasMaxLength(300);
            
            Property(x => x.FileName).IsRequired();

            HasRequired(x => x.User)
                .WithMany(x => x.Medias)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);



            HasRequired(x => x.Lookup_MediaType)
                .WithMany(x => x.Media_Type)
                .HasForeignKey(x => x.LookupId_MediaType)
                .WillCascadeOnDelete(false);

            

            HasRequired(x => x.Writer)
                .WithMany(x => x.Medias)
                .HasForeignKey(x => x.WriterId)
                .WillCascadeOnDelete(false);

            
        }
    }
}
