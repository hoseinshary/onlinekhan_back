namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAssayVarient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assays", "NumberOfVarient", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assays", "NumberOfVarient");
        }
    }
}
