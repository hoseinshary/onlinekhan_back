using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Family).HasMaxLength(50).IsRequired();
            Property(x => x.Username).HasMaxLength(50).IsRequired();
            HasIndex(x => x.Username).IsUnique().HasName("UK_User_Username");
            Property(x => x.Password).HasMaxLength(50).IsRequired();
            Property(x => x.NationalNo).HasMaxLength(10);
            HasIndex(x => x.NationalNo).IsUnique().HasName("UK_User_NationalNo");
            Property(x => x.Phone).HasMaxLength(8);
            Property(x => x.Mobile).HasMaxLength(11);

            HasRequired(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.City)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);


            HasOptional(x => x.Teacher)
                .WithRequired(x => x.User);

            HasOptional(x => x.Student)
                .WithRequired(x => x.User);

            HasMany(x => x.Lessons)
                .WithMany(x => x.Users)
                .Map(config =>
                {
                    config.MapLeftKey("UserId");
                    config.MapRightKey("LessonId");
                    config.ToTable("Lessons_Users");
                });
        }
    }
}
