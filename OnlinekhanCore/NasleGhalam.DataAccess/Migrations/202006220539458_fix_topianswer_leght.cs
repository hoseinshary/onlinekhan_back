namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_topianswer_leght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "TopicAnswer", c => c.String(maxLength: 400));
            DropColumn("dbo.Programs", "Description");
            DropColumn("dbo.ProgramItems", "DayOfWeak");
            DropColumn("dbo.ProgramItems", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProgramItems", "Description", c => c.String(maxLength: 50));
            AddColumn("dbo.ProgramItems", "DayOfWeak", c => c.Int(nullable: false));
            AddColumn("dbo.Programs", "Description", c => c.String(maxLength: 50));
            AlterColumn("dbo.Questions", "TopicAnswer", c => c.String(maxLength: 50));
        }
    }
}
