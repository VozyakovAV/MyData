namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCTerm : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.mem_questions", newName: "mem_terms");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.mem_terms", newName: "mem_questions");
        }
    }
}
