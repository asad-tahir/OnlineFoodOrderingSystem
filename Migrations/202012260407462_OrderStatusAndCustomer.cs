namespace OnlineFoodOrderingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatusAndCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "Status", c => c.String());
            AddColumn("dbo.Transactions", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.Orders", "CustomerId");
            CreateIndex("dbo.Transactions", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transactions", "CustomerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            AlterColumn("dbo.Items", "Description", c => c.String());
            AlterColumn("dbo.Items", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            DropColumn("dbo.Transactions", "CustomerId");
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "CustomerId");
        }
    }
}
