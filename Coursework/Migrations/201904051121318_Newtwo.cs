namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newtwo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Causes", "causeImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Causes", "causeImage", c => c.String());
        }
    }
}
