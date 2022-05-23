namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacherfix2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Teachers", name: "Id1", newName: "Id");
            RenameIndex(table: "dbo.Teachers", name: "IX_Id1", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Teachers", name: "IX_Id", newName: "IX_Id1");
            RenameColumn(table: "dbo.Teachers", name: "Id", newName: "Id1");
        }
    }
}
