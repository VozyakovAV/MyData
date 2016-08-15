namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mem_question",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        question = c.String(nullable: false),
                        answer = c.String(nullable: false),
                        setID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.mem_sets", t => t.setID, cascadeDelete: true)
                .Index(t => t.setID);
            
            CreateTable(
                "dbo.mem_sets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.mem_question", "setID", "dbo.mem_sets");
            DropIndex("dbo.mem_question", new[] { "setID" });
            DropTable("dbo.mem_sets");
            DropTable("dbo.mem_question");
        }
    }
}
