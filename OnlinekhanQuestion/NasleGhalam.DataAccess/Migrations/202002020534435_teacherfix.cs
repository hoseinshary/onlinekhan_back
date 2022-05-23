namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacherfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Teachers", new[] { "UserId" });
            RenameColumn(table: "dbo.Teachers", name: "UserId", newName: "Id1");
            DropPrimaryKey("dbo.Teachers");
            AddPrimaryKey("dbo.Teachers", "Id1");
            CreateIndex("dbo.Teachers", "Id1");
            AddForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers", "Id1");
            DropColumn("dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Teachers", new[] { "Id1" });
            DropPrimaryKey("dbo.Teachers");
            AddPrimaryKey("dbo.Teachers", "Id");
            RenameColumn(table: "dbo.Teachers", name: "Id1", newName: "UserId");
            CreateIndex("dbo.Teachers", "UserId", unique: true);
            AddForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers", "Id");
        }
    }
}
