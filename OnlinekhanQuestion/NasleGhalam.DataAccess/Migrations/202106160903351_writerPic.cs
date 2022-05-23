namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class writerPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "ProfilePic", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Writers", "ProfilePic");
        }
    }
}
