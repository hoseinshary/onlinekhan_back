namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePackageField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Description", c => c.String(maxLength: 300));
            AlterColumn("dbo.Packages", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Packages", "Discription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Discription", c => c.String(maxLength: 50));
            AlterColumn("dbo.Packages", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.Packages", "Description");
        }
    }
}
