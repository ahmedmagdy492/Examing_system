namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSubjectNameToQuestionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "SubjectName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "SubjectName");
        }
    }
}
