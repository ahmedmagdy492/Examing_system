namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finishedScheam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        ExamId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId)
                .Index(t => t.QuestionId)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.SubjectQuestions",
                c => new
                    {
                        SubjectId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        QuestionMark = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubjectId, t.QuestionId })
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.StudentAnswers", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.StudentAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.SubjectQuestions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.StudentAnswers", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectQuestions", new[] { "QuestionId" });
            DropIndex("dbo.SubjectQuestions", new[] { "SubjectId" });
            DropIndex("dbo.StudentAnswers", new[] { "Subject_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "QuestionId" });
            DropIndex("dbo.StudentAnswers", new[] { "ExamId" });
            DropIndex("dbo.StudentAnswers", new[] { "StudentId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.SubjectQuestions");
            DropTable("dbo.StudentAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
