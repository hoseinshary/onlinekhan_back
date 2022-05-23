using System;
using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AssayConfig : EntityTypeConfiguration<Assay>
    {
        public AssayConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(50).IsRequired();
            Property(x => x.QuestionsFile1).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsFile2).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsFile3).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsFile4).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsAnswer1).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsAnswer2).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsAnswer3).HasMaxLength(Int32.MaxValue);
            Property(x => x.QuestionsAnswer4).HasMaxLength(Int32.MaxValue);


            HasRequired(x=>x.User)
                .WithMany(x=>x.Assays)
                .HasForeignKey(x=>x.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_QuestionType)
                .WithMany(x => x.Assay_QuestionType)
                .HasForeignKey(x => x.LookupId_QuestionType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_Importance)
                .WithMany(x => x.Assay_Importance)
                .HasForeignKey(x => x.LookupId_Importance)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_Type)
                .WithMany(x => x.Assay_Type)
                .HasForeignKey(x => x.LookupId_Type)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Lessons)
                .WithMany(x => x.Assays)
                .Map(config =>
                {
                    config.MapLeftKey("AssayId");
                    config.MapRightKey("LessonId");
                    config.ToTable("Assays_Lessons");

                });



        }
    }
}
