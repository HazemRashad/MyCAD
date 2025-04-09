namespace ShapeDrawer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Shapes",
                c => new
                    {
                        ShapeId = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Radius = c.Double(),
                        X1 = c.Int(),
                        Y1 = c.Int(),
                        X2 = c.Int(),
                        Y2 = c.Int(),
                        Width = c.Double(),
                        Height = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ShapeId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Shapes", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Shapes", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Shapes");
            DropTable("dbo.Projects");
        }
    }
}
