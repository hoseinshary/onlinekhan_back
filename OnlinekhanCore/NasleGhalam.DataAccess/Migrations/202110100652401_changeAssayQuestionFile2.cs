namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAssayQuestionFile2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assays", "QuestionsFile1", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsFile2", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsFile3", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsFile4", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsAnswer1", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsAnswer2", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsAnswer3", c => c.String(maxLength: 50));
            AddColumn("dbo.Assays", "QuestionsAnswer4", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assays", "QuestionsAnswer4");
            DropColumn("dbo.Assays", "QuestionsAnswer3");
            DropColumn("dbo.Assays", "QuestionsAnswer2");
            DropColumn("dbo.Assays", "QuestionsAnswer1");
            DropColumn("dbo.Assays", "QuestionsFile4");
            DropColumn("dbo.Assays", "QuestionsFile3");
            DropColumn("dbo.Assays", "QuestionsFile2");
            DropColumn("dbo.Assays", "QuestionsFile1");
        }
    }
}
