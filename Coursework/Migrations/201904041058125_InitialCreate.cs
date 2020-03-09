namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Causes",
                c => new
                    {
                        CauseID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 160),
                        Description = c.String(nullable: false),
                        createdBy = c.String(),
                        createdOn = c.DateTime(nullable: false),
                        Target = c.Int(nullable: false),
                        causeImage = c.String(),
                    })
                .PrimaryKey(t => t.CauseID);
            
            CreateTable(
                "dbo.Signatures",
                c => new
                    {
                        SignatureID = c.Int(nullable: false, identity: true),
                        CauseID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        signedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SignatureID)
                .ForeignKey("dbo.Causes", t => t.CauseID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CauseID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserImage = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Signatures", "UserID", "dbo.Users");
            DropForeignKey("dbo.Signatures", "CauseID", "dbo.Causes");
            DropIndex("dbo.Signatures", new[] { "UserID" });
            DropIndex("dbo.Signatures", new[] { "CauseID" });
            DropTable("dbo.Users");
            DropTable("dbo.Signatures");
            DropTable("dbo.Causes");
        }
    }
}
