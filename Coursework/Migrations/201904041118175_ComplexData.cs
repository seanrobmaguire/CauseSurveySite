namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            Sql("INSERT INTO dbo.Categories (Name) VALUES ('Other')");
            AddColumn("dbo.Causes", "CategoryID", c => c.Int(nullable: false, defaultValue: 1));
          //  AddColumn("dbo.Causes", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Causes", "CategoryID");
            AddForeignKey("dbo.Causes", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Causes", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Causes", new[] { "CategoryID" });
            DropColumn("dbo.Causes", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
