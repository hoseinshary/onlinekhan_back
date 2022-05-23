namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionupdateuser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions_Users",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.UserId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions_Users", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions_Users", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions_Users", new[] { "UserId" });
            DropIndex("dbo.Questions_Users", new[] { "QuestionId" });
            DropTable("dbo.Questions_Users");
        }
    }
}
