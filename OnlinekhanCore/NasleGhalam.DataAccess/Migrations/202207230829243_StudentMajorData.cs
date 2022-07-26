namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentMajorData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "BirthYear", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "SahmieNahayei", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Field", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "DiplomYear", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "ProvinceBoomyId", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "NahyeBoomy", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "GhotbBoomy", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "IsAllowedRoozane", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedMajazi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedPayam", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedAzad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "GorohAzmayeshi", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "IsAllowedRozaneGoroh", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedMajaziGoroh", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedFarhangianGoroh", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedPayamGoroh", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsAllowedAzadGoroh", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "SahmieNahayeiOne", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "SahmieNahayeiTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "SahmieNahayeiThree", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "SahmieNahayeiFour", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "SahmieNahayeiFive", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KeshvariOne", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KeshvariTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KeshvariThree", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KeshvariFour", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KeshvariFive", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KolOne", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KolTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KolThree", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KolFour", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "KolFive", c => c.Int(nullable: false));
            AlterColumn("dbo.AssayAnswerSheets", "AnswerTimes", c => c.String());
            AlterColumn("dbo.AssayAnswerSheets", "Answers", c => c.String());
            AlterColumn("dbo.AssayAnswerSheets", "MaybeList", c => c.String());
            AlterColumn("dbo.AssayAnswerSheets", "AfterList", c => c.String());
            AlterColumn("dbo.AssayAnswerSheets", "CantList", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AssayAnswerSheets", "CantList", c => c.String(maxLength: 50));
            AlterColumn("dbo.AssayAnswerSheets", "AfterList", c => c.String(maxLength: 50));
            AlterColumn("dbo.AssayAnswerSheets", "MaybeList", c => c.String(maxLength: 50));
            AlterColumn("dbo.AssayAnswerSheets", "Answers", c => c.String(maxLength: 50));
            AlterColumn("dbo.AssayAnswerSheets", "AnswerTimes", c => c.String(maxLength: 50));
            DropColumn("dbo.Students", "KolFive");
            DropColumn("dbo.Students", "KolFour");
            DropColumn("dbo.Students", "KolThree");
            DropColumn("dbo.Students", "KolTwo");
            DropColumn("dbo.Students", "KolOne");
            DropColumn("dbo.Students", "KeshvariFive");
            DropColumn("dbo.Students", "KeshvariFour");
            DropColumn("dbo.Students", "KeshvariThree");
            DropColumn("dbo.Students", "KeshvariTwo");
            DropColumn("dbo.Students", "KeshvariOne");
            DropColumn("dbo.Students", "SahmieNahayeiFive");
            DropColumn("dbo.Students", "SahmieNahayeiFour");
            DropColumn("dbo.Students", "SahmieNahayeiThree");
            DropColumn("dbo.Students", "SahmieNahayeiTwo");
            DropColumn("dbo.Students", "SahmieNahayeiOne");
            DropColumn("dbo.Students", "IsAllowedAzadGoroh");
            DropColumn("dbo.Students", "IsAllowedPayamGoroh");
            DropColumn("dbo.Students", "IsAllowedFarhangianGoroh");
            DropColumn("dbo.Students", "IsAllowedMajaziGoroh");
            DropColumn("dbo.Students", "IsAllowedRozaneGoroh");
            DropColumn("dbo.Students", "GorohAzmayeshi");
            DropColumn("dbo.Students", "IsAllowedAzad");
            DropColumn("dbo.Students", "IsAllowedPayam");
            DropColumn("dbo.Students", "IsAllowedMajazi");
            DropColumn("dbo.Students", "IsAllowedRoozane");
            DropColumn("dbo.Students", "GhotbBoomy");
            DropColumn("dbo.Students", "NahyeBoomy");
            DropColumn("dbo.Students", "ProvinceBoomyId");
            DropColumn("dbo.Students", "DiplomYear");
            DropColumn("dbo.Students", "Field");
            DropColumn("dbo.Students", "SahmieNahayei");
            DropColumn("dbo.Students", "BirthYear");
            DropColumn("dbo.Students", "Gender");
        }
    }
}
