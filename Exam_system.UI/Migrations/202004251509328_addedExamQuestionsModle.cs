namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedExamQuestionsModle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamQuestions",
                c => new
                    {
                        ExamId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExamId, t.QuestionId })
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.ExamId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.ExamQuestions", "ExamId", "dbo.Exams");
            DropIndex("dbo.ExamQuestions", new[] { "QuestionId" });
            DropIndex("dbo.ExamQuestions", new[] { "ExamId" });
            DropTable("dbo.ExamQuestions");
        }
    }
}
