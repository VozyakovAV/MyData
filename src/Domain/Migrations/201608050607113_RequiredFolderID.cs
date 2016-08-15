namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFolderID : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.mem_sets", new[] { "folderID" });
            AlterColumn("dbo.mem_sets", "folderID", c => c.Int(nullable: false));
            CreateIndex("dbo.mem_sets", "folderID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.mem_sets", new[] { "folderID" });
            AlterColumn("dbo.mem_sets", "folderID", c => c.Int());
            CreateIndex("dbo.mem_sets", "folderID");
        }
    }
}
