namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.pln_tasks", "description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.pln_tasks", "description", c => c.String(nullable: false));
        }
    }
}
