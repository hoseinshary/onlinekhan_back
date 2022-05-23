namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAassayQuestionLessonId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssayQuestions", "LessonId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssayQuestions", "LessonId");
        }
    }
}
