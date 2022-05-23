namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixquestionAnswerJudge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionAnswers", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionAnswers", "IsUpdate", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionAnswerJudges", "IsUpdate", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionAnswerJudges", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionAnswerJudges", "IsDelete");
            DropColumn("dbo.QuestionAnswerJudges", "IsUpdate");
            DropColumn("dbo.QuestionAnswers", "IsUpdate");
            DropColumn("dbo.QuestionAnswers", "IsDelete");
        }
    }
}
