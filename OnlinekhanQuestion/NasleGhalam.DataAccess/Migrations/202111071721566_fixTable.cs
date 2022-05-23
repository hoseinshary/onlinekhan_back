namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssayAnswerSheets", "AssaySchaduleId", "dbo.AssaySchedules");
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssaySchaduleId" });
            DropColumn("dbo.AssayAnswerSheets", "AssaySchaduleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssayAnswerSheets", "AssaySchaduleId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssayAnswerSheets", "AssaySchaduleId");
            AddForeignKey("dbo.AssayAnswerSheets", "AssaySchaduleId", "dbo.AssaySchedules", "Id", cascadeDelete: true);
        }
    }
}
