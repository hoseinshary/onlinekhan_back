namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixAssayCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssayQuestions", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.AssayAnswerSheets", "AssayQuestionId", "dbo.AssayQuestions");
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssaySchaduleId" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssayQuestionId" });
            DropIndex("dbo.AssayQuestions", new[] { "LessonId" });
            RenameColumn(table: "dbo.AssayAnswerSheets", name: "AssaySchaduleId", newName: "AssaySchedule_Id");
         
            
            
            
          
          
            
     
            
            CreateTable(
                "dbo.Assays_Lessons",
                c => new
                    {
                        AssayId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AssayId, t.LessonId })
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.AssayId)
                .Index(t => t.LessonId);
            
            AddColumn("dbo.AssayAnswerSheets", "AssayId", c => c.Int(nullable: false));
            AddColumn("dbo.AssayAnswerSheets", "AssayVarient", c => c.Int(nullable: false));
            AddColumn("dbo.AssayAnswerSheets", "AssayTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AssayAnswerSheets", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Assays", "File1", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "File2", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "File3", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "File4", c => c.String(maxLength: 50));
            AlterColumn("dbo.AssayAnswerSheets", "AssaySchedule_Id", c => c.Int());
            CreateIndex("dbo.AssayAnswerSheets", "AssayId");
            CreateIndex("dbo.AssayAnswerSheets", "AssaySchedule_Id");
            AddForeignKey("dbo.AssayAnswerSheets", "AssayId", "dbo.Assays", "Id");
            DropColumn("dbo.AssayAnswerSheets", "AssayQuestionId");
            DropColumn("dbo.AssayAnswerSheets", "AnswerTime");
            DropColumn("dbo.AssayAnswerSheets", "Answer");
            DropColumn("dbo.AssayQuestions", "LessonId");
            DropColumn("dbo.Assays", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assays", "File", c => c.String(maxLength: 50));
            AddColumn("dbo.AssayQuestions", "LessonId", c => c.Int(nullable: false));
            AddColumn("dbo.AssayAnswerSheets", "Answer", c => c.Int(nullable: false));
            AddColumn("dbo.AssayAnswerSheets", "AnswerTime", c => c.Int(nullable: false));
            AddColumn("dbo.AssayAnswerSheets", "AssayQuestionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AssayAnswerSheets", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Assays_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Assays_Lessons", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.StudentMajorlists", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentMajorlist_Majors", "MajorsId", "dbo.Majors");
            DropForeignKey("dbo.StudentMajorlist_Majors", "StudentMajorlistId", "dbo.StudentMajorlists");
            DropForeignKey("dbo.StudentMajorList_Major", "StudentMajorListId", "dbo.StudentMajorlists");
            DropForeignKey("dbo.StudentMajorList_Major", "MajorsId", "dbo.Majors");
            DropIndex("dbo.Assays_Lessons", new[] { "LessonId" });
            DropIndex("dbo.Assays_Lessons", new[] { "AssayId" });
            DropIndex("dbo.StudentMajorlist_Majors", new[] { "MajorsId" });
            DropIndex("dbo.StudentMajorlist_Majors", new[] { "StudentMajorlistId" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.StudentMajorList_Major", new[] { "MajorsId" });
            DropIndex("dbo.StudentMajorList_Major", new[] { "StudentMajorListId" });
            DropIndex("dbo.StudentMajorlists", new[] { "StudentId" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssaySchedule_Id" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssayId" });
            AlterColumn("dbo.AssayAnswerSheets", "AssaySchedule_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Assays", "File4");
            DropColumn("dbo.Assays", "File3");
            DropColumn("dbo.Assays", "File2");
            DropColumn("dbo.Assays", "File1");
            DropColumn("dbo.AssayAnswerSheets", "DateTime");
            DropColumn("dbo.AssayAnswerSheets", "AssayTime");
            DropColumn("dbo.AssayAnswerSheets", "AssayVarient");
            DropColumn("dbo.AssayAnswerSheets", "AssayId");
            DropTable("dbo.Assays_Lessons");
            DropTable("dbo.StudentMajorlist_Majors");
            DropTable("dbo.Logs");
            DropTable("dbo.StudentMajorList_Major");
            DropTable("dbo.Majors");
            DropTable("dbo.StudentMajorlists");
            RenameColumn(table: "dbo.AssayAnswerSheets", name: "AssaySchedule_Id", newName: "AssaySchaduleId");
            CreateIndex("dbo.AssayQuestions", "LessonId");
            CreateIndex("dbo.AssayAnswerSheets", "AssayQuestionId");
            CreateIndex("dbo.AssayAnswerSheets", "AssaySchaduleId");
            AddForeignKey("dbo.AssayAnswerSheets", "AssayQuestionId", "dbo.AssayQuestions", "Id");
            AddForeignKey("dbo.AssayQuestions", "LessonId", "dbo.Lessons", "Id");
        }
    }
}
