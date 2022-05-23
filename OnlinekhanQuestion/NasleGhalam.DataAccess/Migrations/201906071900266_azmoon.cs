namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class azmoon : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Writers", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.Writers", name: "IX_UserId", newName: "IX_User_Id");
            CreateTable(
                "dbo.AssayAnswerSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssaySchaduleId = c.Int(nullable: false),
                        AssayQuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AnswerTime = c.Int(nullable: false),
                        Answer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssayQuestions", t => t.AssayQuestionId)
                .ForeignKey("dbo.AssaySchadules", t => t.AssaySchaduleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.AssaySchaduleId)
                .Index(t => t.AssayQuestionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AssayQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssayId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                        File = c.String(maxLength: 50),
                        AnswerNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.AssayId)
                .Index(t => t.QuestionId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Assays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Time = c.Int(nullable: false),
                        LookupId_Importance = c.Int(nullable: false),
                        LookupId_Type = c.Int(nullable: false),
                        LookupId_QuestionType = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        IsOnline = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateTimeCreate = c.DateTime(nullable: false),
                        File = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_Importance)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_Type)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LookupId_Importance)
                .Index(t => t.LookupId_Type)
                .Index(t => t.LookupId_QuestionType)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AssaySchadules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssayId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .Index(t => t.AssayId);
            
            AlterColumn("dbo.Writers", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssayAnswerSheets", "UserId", "dbo.Users");
            DropForeignKey("dbo.AssayAnswerSheets", "AssaySchaduleId", "dbo.AssaySchadules");
            DropForeignKey("dbo.AssayAnswerSheets", "AssayQuestionId", "dbo.AssayQuestions");
            DropForeignKey("dbo.AssayQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AssayQuestions", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.AssayQuestions", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Assays", "UserId", "dbo.Users");
            DropForeignKey("dbo.Assays", "LookupId_Type", "dbo.Lookups");
            DropForeignKey("dbo.Assays", "LookupId_QuestionType", "dbo.Lookups");
            DropForeignKey("dbo.Assays", "LookupId_Importance", "dbo.Lookups");
            DropForeignKey("dbo.AssaySchadules", "AssayId", "dbo.Assays");
            DropIndex("dbo.AssaySchadules", new[] { "AssayId" });
            DropIndex("dbo.Assays", new[] { "UserId" });
            DropIndex("dbo.Assays", new[] { "LookupId_QuestionType" });
            DropIndex("dbo.Assays", new[] { "LookupId_Type" });
            DropIndex("dbo.Assays", new[] { "LookupId_Importance" });
            DropIndex("dbo.AssayQuestions", new[] { "LessonId" });
            DropIndex("dbo.AssayQuestions", new[] { "QuestionId" });
            DropIndex("dbo.AssayQuestions", new[] { "AssayId" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "UserId" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssayQuestionId" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssaySchaduleId" });
            AlterColumn("dbo.Writers", "Name", c => c.String(nullable: false, maxLength: 150));
            DropTable("dbo.AssaySchadules");
            DropTable("dbo.Assays");
            DropTable("dbo.AssayQuestions");
            DropTable("dbo.AssayAnswerSheets");
            RenameIndex(table: "dbo.Writers", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Writers", name: "User_Id", newName: "UserId");
        }
    }
}
