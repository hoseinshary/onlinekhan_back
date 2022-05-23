namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.TeacherGroup_Students",
                c => new
                    {
                        TeacherGroupId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherGroupId, t.StudentId })
                .ForeignKey("dbo.TeacherGroups", t => t.TeacherGroupId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.TeacherGroupId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherGroups", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherGroup_Students", "StudentId", "dbo.Students");
            DropForeignKey("dbo.TeacherGroup_Students", "TeacherGroupId", "dbo.TeacherGroups");
            DropIndex("dbo.TeacherGroup_Students", new[] { "StudentId" });
            DropIndex("dbo.TeacherGroup_Students", new[] { "TeacherGroupId" });
            DropIndex("dbo.TeacherGroups", new[] { "TeacherId" });
            DropTable("dbo.TeacherGroup_Students");
            DropTable("dbo.TeacherGroups");
        }
    }
}
