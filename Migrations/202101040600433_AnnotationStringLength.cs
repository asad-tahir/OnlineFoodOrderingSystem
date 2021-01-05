namespace OnlineFoodOrderingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotationStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Items", "Description", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Orders", "Status", c => c.String(maxLength: 15));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.Orders", "Status", c => c.String());
            AlterColumn("dbo.Items", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
    }
}
