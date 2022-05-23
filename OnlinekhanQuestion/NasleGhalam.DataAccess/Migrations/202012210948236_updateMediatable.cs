namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMediatable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "CoverImage", c => c.String(maxLength: 50));
            AddColumn("dbo.Media", "Length", c => c.String(maxLength: 50));
            AddColumn("dbo.Media", "YearOfBook", c => c.Int(nullable: false));
            AddColumn("dbo.Media", "PagesOfBook", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "PagesOfBook");
            DropColumn("dbo.Media", "YearOfBook");
            DropColumn("dbo.Media", "Length");
            DropColumn("dbo.Media", "CoverImage");
        }
    }
}
