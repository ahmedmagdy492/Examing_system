namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTakenExamPropToStudentsExamModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentExams", "TakenExamDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentExams", "TakenExamDate");
        }
    }
}
