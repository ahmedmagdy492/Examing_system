namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsAvailablePropToExamAndAddedTookExamCountToStudentsExams : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "IsAvailable", c => c.Boolean());
            AddColumn("dbo.StudentExams", "TookExamCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentExams", "TookExamCount");
            DropColumn("dbo.Exams", "IsAvailable");
        }
    }
}
