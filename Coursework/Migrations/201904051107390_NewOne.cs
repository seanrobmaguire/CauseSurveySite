namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewOne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "catImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "catImage");
        }
    }
}
