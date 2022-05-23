namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionMultiAreaType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "LookupId_AreaType", "dbo.Lookups");
            DropIndex("dbo.Questions", new[] { "LookupId_AreaType" });
            CreateTable(
                "dbo.Questions_AreaTypes",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        AreaTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.AreaTypeId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Lookups", t => t.AreaTypeId)
                .Index(t => t.QuestionId)
                .Index(t => t.AreaTypeId);
            
           // AddColumn("dbo.Programs", "Description", c => c.String(maxLength: 50));
            //AddColumn("dbo.ProgramItems", "DayOfWeak", c => c.Int(nullable: false));
            //AddColumn("dbo.ProgramItems", "Description", c => c.String(maxLength: 50));
            DropColumn("dbo.Questions", "LookupId_AreaType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "LookupId_AreaType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questions_AreaTypes", "AreaTypeId", "dbo.Lookups");
            DropForeignKey("dbo.Questions_AreaTypes", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions_AreaTypes", new[] { "AreaTypeId" });
            DropIndex("dbo.Questions_AreaTypes", new[] { "QuestionId" });
            DropColumn("dbo.ProgramItems", "Description");
            DropColumn("dbo.ProgramItems", "DayOfWeak");
            DropColumn("dbo.Programs", "Description");
            DropTable("dbo.Questions_AreaTypes");
            CreateIndex("dbo.Questions", "LookupId_AreaType");
            AddForeignKey("dbo.Questions", "LookupId_AreaType", "dbo.Lookups", "Id");
        }
    }
}
