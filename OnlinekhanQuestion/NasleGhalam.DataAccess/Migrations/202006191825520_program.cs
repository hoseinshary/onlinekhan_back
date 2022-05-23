namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class program : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        StudentId = c.Int(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.ProgramItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupId_PrgoramItemName = c.Int(nullable: false),
                        ProgramId = c.Int(nullable: false),
                        Hour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_PrgoramItemName)
                .ForeignKey("dbo.Programs", t => t.ProgramId)
                .Index(t => t.LookupId_PrgoramItemName)
                .Index(t => t.ProgramId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programs", "StudentId", "dbo.Students");
            DropForeignKey("dbo.ProgramItems", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.ProgramItems", "LookupId_PrgoramItemName", "dbo.Lookups");
            DropIndex("dbo.ProgramItems", new[] { "ProgramId" });
            DropIndex("dbo.ProgramItems", new[] { "LookupId_PrgoramItemName" });
            DropIndex("dbo.Programs", new[] { "StudentId" });
            DropTable("dbo.ProgramItems");
            DropTable("dbo.Programs");
        }
    }
}
