namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.pln_projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        dateClosed = c.DateTime(),
                        deadline = c.DateTime(),
                        status = c.Int(nullable: false),
                        isArchive = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        parentID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.pln_projects", t => t.parentID)
                .Index(t => t.parentID);
            
            CreateTable(
                "dbo.pln_tasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        dateClosed = c.DateTime(),
                        deadline = c.DateTime(),
                        status = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        projectID = c.Int(),
                        iterationID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.pln_iteration", t => t.iterationID)
                .ForeignKey("dbo.pln_projects", t => t.projectID, cascadeDelete: true)
                .Index(t => t.projectID)
                .Index(t => t.iterationID);
            
            CreateTable(
                "dbo.pln_iteration",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        dateStart = c.DateTime(nullable: false),
                        dateFinish = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pln_tasks", "projectID", "dbo.pln_projects");
            DropForeignKey("dbo.pln_tasks", "iterationID", "dbo.pln_iteration");
            DropForeignKey("dbo.pln_projects", "parentID", "dbo.pln_projects");
            DropIndex("dbo.pln_tasks", new[] { "iterationID" });
            DropIndex("dbo.pln_tasks", new[] { "projectID" });
            DropIndex("dbo.pln_projects", new[] { "parentID" });
            DropTable("dbo.pln_iteration");
            DropTable("dbo.pln_tasks");
            DropTable("dbo.pln_projects");
        }
    }
}
