namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.pln_iteration", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.pln_iteration", "name", c => c.String(nullable: false));
        }
    }
}
