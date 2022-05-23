namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionanswer_change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions_Users", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions_Users", "UserId", "dbo.Users");
            DropIndex("dbo.Questions", new[] { "UserId" });
            DropIndex("dbo.Questions_Users", new[] { "QuestionId" });
            DropIndex("dbo.Questions_Users", new[] { "UserId" });
            CreateTable(
                "dbo.QuestionUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        QuestionActivity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.QuestionAnswers", "QuestionAnswerType", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "UserId");
            DropTable("dbo.Questions_Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Questions_Users",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.UserId });
            
            AddColumn("dbo.Questions", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.QuestionUpdates", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionUpdates", "QuestionId", "dbo.Questions");
            DropIndex("dbo.QuestionUpdates", new[] { "QuestionId" });
            DropIndex("dbo.QuestionUpdates", new[] { "UserId" });
            DropColumn("dbo.QuestionAnswers", "QuestionAnswerType");
            DropTable("dbo.QuestionUpdates");
            CreateIndex("dbo.Questions_Users", "UserId");
            CreateIndex("dbo.Questions_Users", "QuestionId");
            CreateIndex("dbo.Questions", "UserId");
            AddForeignKey("dbo.Questions_Users", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Questions_Users", "QuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "UserId", "dbo.Users", "Id");
        }
    }
}
