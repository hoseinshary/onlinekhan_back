namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "TimeDays", c => c.Int(nullable: false));
            DropColumn("dbo.Packages", "Expire");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Expire", c => c.DateTime(nullable: false));
            DropColumn("dbo.Packages", "TimeDays");
        }
    }
}
