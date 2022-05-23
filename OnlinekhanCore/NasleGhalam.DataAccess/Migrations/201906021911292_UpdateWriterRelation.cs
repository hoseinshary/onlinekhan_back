namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWriterRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Writers", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Writers", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.Writers", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "Name", c => c.String(nullable: false, maxLength: 50));
            RenameIndex(table: "dbo.Writers", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Writers", name: "UserId", newName: "User_Id");
        }
    }
}
