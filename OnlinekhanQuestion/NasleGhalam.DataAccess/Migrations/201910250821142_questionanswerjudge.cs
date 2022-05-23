namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class questionanswerjudge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionAnswerJudges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActiveQuestionAnswer = c.Boolean(nullable: false),
                        QuestionAnswerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LessonName = c.String(maxLength: 50),
                        LookupId_ReasonProblem = c.Int(nullable: false),
                        Description = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_ReasonProblem)
                .ForeignKey("dbo.QuestionAnswers", t => t.QuestionAnswerId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.QuestionAnswerId)
                .Index(t => t.UserId)
                .Index(t => t.LookupId_ReasonProblem);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionAnswerJudges", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswerJudges", "QuestionAnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.QuestionAnswerJudges", "LookupId_ReasonProblem", "dbo.Lookups");
            DropIndex("dbo.QuestionAnswerJudges", new[] { "LookupId_ReasonProblem" });
            DropIndex("dbo.QuestionAnswerJudges", new[] { "UserId" });
            DropIndex("dbo.QuestionAnswerJudges", new[] { "QuestionAnswerId" });
            DropTable("dbo.QuestionAnswerJudges");
        }
    }
}
