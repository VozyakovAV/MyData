namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.pln_projects", "isArchive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.pln_projects", "isArchive", c => c.Boolean(nullable: false));
        }
    }
}
