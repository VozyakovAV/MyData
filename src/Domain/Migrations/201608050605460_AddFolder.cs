namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFolder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mem_folders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.mem_sets", "folderID", c => c.Int());
            CreateIndex("dbo.mem_sets", "folderID");
            AddForeignKey("dbo.mem_sets", "folderID", "dbo.mem_folders", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.mem_sets", "folderID", "dbo.mem_folders");
            DropIndex("dbo.mem_sets", new[] { "folderID" });
            DropColumn("dbo.mem_sets", "folderID");
            DropTable("dbo.mem_folders");
        }
    }
}
