namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_questionasnwer_judge_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionAnswerJudges", "IsMaster", c => c.Boolean(nullable: false));
            DropColumn("dbo.QuestionAnswerJudges", "LessonName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionAnswerJudges", "LessonName", c => c.String(maxLength: 50));
            DropColumn("dbo.QuestionAnswerJudges", "IsMaster");
        }
    }
}
