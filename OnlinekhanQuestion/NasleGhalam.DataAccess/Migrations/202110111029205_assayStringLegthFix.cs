namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assayStringLegthFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assays", "QuestionsFile1", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsFile2", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsFile3", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsFile4", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsAnswer1", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsAnswer2", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsAnswer3", c => c.String());
            AlterColumn("dbo.Assays", "QuestionsAnswer4", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assays", "QuestionsAnswer4", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsAnswer3", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsAnswer2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsAnswer1", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsFile4", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsFile3", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsFile2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Assays", "QuestionsFile1", c => c.String(maxLength: 50));
        }
    }
}
