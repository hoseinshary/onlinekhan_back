namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixassayAnswerSheet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssayAnswerSheets", "AssaySchedule_Id", "dbo.AssaySchedules");
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssaySchedule_Id" });
            RenameColumn(table: "dbo.AssayAnswerSheets", name: "AssaySchedule_Id", newName: "AssaySchaduleId");
            AddColumn("dbo.AssayAnswerSheets", "AnswerTimes", c => c.String(maxLength: 50));
            AddColumn("dbo.AssayAnswerSheets", "Answers", c => c.String(maxLength: 50));
            AddColumn("dbo.AssayAnswerSheets", "MaybeList", c => c.String(maxLength: 50));
            AddColumn("dbo.AssayAnswerSheets", "AgterfList", c => c.String(maxLength: 50));
            AddColumn("dbo.AssayAnswerSheets", "CantList", c => c.String(maxLength: 50));
            AlterColumn("dbo.AssayAnswerSheets", "AssaySchaduleId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssayAnswerSheets", "AssaySchaduleId");
            //AddForeignKey("dbo.AssayAnswerSheets", "AssaySchaduleId", "dbo.AssaySchedules", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssayAnswerSheets", "AssaySchaduleId", "dbo.AssaySchedules");
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssaySchaduleId" });
            AlterColumn("dbo.AssayAnswerSheets", "AssaySchaduleId", c => c.Int());
            DropColumn("dbo.AssayAnswerSheets", "CantList");
            DropColumn("dbo.AssayAnswerSheets", "AgterfList");
            DropColumn("dbo.AssayAnswerSheets", "MaybeList");
            DropColumn("dbo.AssayAnswerSheets", "Answers");
            DropColumn("dbo.AssayAnswerSheets", "AnswerTimes");
            RenameColumn(table: "dbo.AssayAnswerSheets", name: "AssaySchaduleId", newName: "AssaySchedule_Id");
            CreateIndex("dbo.AssayAnswerSheets", "AssaySchedule_Id");
            AddForeignKey("dbo.AssayAnswerSheets", "AssaySchedule_Id", "dbo.AssaySchedules", "Id");
        }
    }
}
