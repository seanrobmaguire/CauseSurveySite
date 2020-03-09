namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Causes", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Causes", "UserID");
        }
    }
}
