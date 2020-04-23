namespace Exam_system.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcreateDateToSubjectModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "CreationDate");
        }
    }
}
