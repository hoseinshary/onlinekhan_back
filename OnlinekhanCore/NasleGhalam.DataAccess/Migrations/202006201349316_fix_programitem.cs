namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_programitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "Description", c => c.String(maxLength: 50));
            AddColumn("dbo.ProgramItems", "DayOfWeak", c => c.Int(nullable: false));
            AddColumn("dbo.ProgramItems", "Description", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProgramItems", "Description");
            DropColumn("dbo.ProgramItems", "DayOfWeak");
            DropColumn("dbo.Programs", "Description");
        }
    }
}
