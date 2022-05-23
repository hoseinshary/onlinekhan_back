namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLessonFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "File", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "File");
        }
    }
}
