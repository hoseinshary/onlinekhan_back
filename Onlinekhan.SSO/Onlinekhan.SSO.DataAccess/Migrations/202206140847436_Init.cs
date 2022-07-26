namespace Onlinekhan.SSO.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Province_Name");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        NationalNo = c.String(maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 8),
                        Mobile = c.String(maxLength: 11),
                        IsAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        ProfilePic = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.Username, unique: true, name: "UK_User_Username")
                .Index(t => t.NationalNo, unique: true, name: "UK_User_NationalNo")
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Ip = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableName = c.String(nullable: false, maxLength: 150),
                        CrudType = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ObjectValue = c.String(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Logins", "User_id", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Logins", new[] { "User_id" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Users", "UK_User_NationalNo");
            DropIndex("dbo.Users", "UK_User_Username");
            DropIndex("dbo.Provinces", "UK_Province_Name");
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropTable("dbo.Sites");
            DropTable("dbo.Logs");
            DropTable("dbo.Logins");
            DropTable("dbo.Users");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
        }
    }
}
