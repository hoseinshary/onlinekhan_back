namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requerdfieldinteacher : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "FatherName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Teachers", "Address", c => c.String(maxLength: 300));
            AlterColumn("dbo.Students", "FatherName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Students", "Address", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Address", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Students", "FatherName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Teachers", "Address", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Teachers", "FatherName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
