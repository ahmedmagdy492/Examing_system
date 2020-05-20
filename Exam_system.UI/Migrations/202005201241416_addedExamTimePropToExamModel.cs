namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedExamTimePropToExamModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "ExamTime", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exams", "ExamTime");
        }
    }
}
