﻿namespace ShapeDrawer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shapes", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shapes", "Name");
        }
    }
}
