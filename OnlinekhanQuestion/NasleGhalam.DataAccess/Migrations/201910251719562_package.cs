namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class package : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sale_Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountItem = c.Int(nullable: false),
                        PriceRow = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        SaleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.PackageId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        IsDelete = c.Boolean(nullable: false),
                        ImageFile = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        Price = c.Int(nullable: false),
                        Expire = c.DateTime(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        Discription = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Packages_Assays",
                c => new
                    {
                        PackageId = c.Int(nullable: false),
                        AssayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PackageId, t.AssayId })
                .ForeignKey("dbo.Packages", t => t.PackageId)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .Index(t => t.PackageId)
                .Index(t => t.AssayId);
            
            CreateTable(
                "dbo.Packages_Lessons",
                c => new
                    {
                        PackageId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PackageId, t.LessonId })
                .ForeignKey("dbo.Packages", t => t.PackageId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.PackageId)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sale_Package", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sale_Package", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Packages_Lessons", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages_Assays", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Packages_Assays", "PackageId", "dbo.Packages");
            DropIndex("dbo.Packages_Lessons", new[] { "LessonId" });
            DropIndex("dbo.Packages_Lessons", new[] { "PackageId" });
            DropIndex("dbo.Packages_Assays", new[] { "AssayId" });
            DropIndex("dbo.Packages_Assays", new[] { "PackageId" });
            DropIndex("dbo.Sale_Package", new[] { "SaleId" });
            DropIndex("dbo.Sale_Package", new[] { "PackageId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropTable("dbo.Packages_Lessons");
            DropTable("dbo.Packages_Assays");
            DropTable("dbo.Packages");
            DropTable("dbo.Sale_Package");
            DropTable("dbo.Sales");
        }
    }
}
