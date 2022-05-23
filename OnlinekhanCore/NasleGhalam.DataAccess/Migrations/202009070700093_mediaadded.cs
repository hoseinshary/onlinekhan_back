namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mediaadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        LookupId_MediaType = c.Int(nullable: false),
                        WriterId = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        FileName = c.String(nullable: false, maxLength: 50),
                        Price = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_MediaType)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Writers", t => t.WriterId)
                .Index(t => t.LookupId_MediaType)
                .Index(t => t.WriterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Topics_Medias",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        MediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.MediaId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .Index(t => t.TopicId)
                .Index(t => t.MediaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Media", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Media", "UserId", "dbo.Users");
            DropForeignKey("dbo.Topics_Medias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Topics_Medias", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Media", "LookupId_MediaType", "dbo.Lookups");
            DropIndex("dbo.Topics_Medias", new[] { "MediaId" });
            DropIndex("dbo.Topics_Medias", new[] { "TopicId" });
            DropIndex("dbo.Media", new[] { "UserId" });
            DropIndex("dbo.Media", new[] { "WriterId" });
            DropIndex("dbo.Media", new[] { "LookupId_MediaType" });
            DropTable("dbo.Topics_Medias");
            DropTable("dbo.Media");
        }
    }
}
