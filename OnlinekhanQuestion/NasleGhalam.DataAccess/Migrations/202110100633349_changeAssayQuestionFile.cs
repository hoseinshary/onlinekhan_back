namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAssayQuestionFile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AssayQuestions", "File");
            DropColumn("dbo.AssayQuestions", "AnswerNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssayQuestions", "AnswerNumber", c => c.Int(nullable: false));
            AddColumn("dbo.AssayQuestions", "File", c => c.String(maxLength: 50));
        }
    }
}
