namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameMemQuestions : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.mem_question", newName: "mem_questions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.mem_questions", newName: "mem_question");
        }
    }
}
