namespace BasicMessageSender.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockedUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlockerUserId = c.Int(nullable: false),
                        BlockedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.BlockerUserId)
                .ForeignKey("dbo.Users", t => t.BlockedUserId, cascadeDelete: true)
                .Index(t => t.BlockerUserId)
                .Index(t => t.BlockedUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sent = c.DateTime(nullable: false),
                        Data = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReceiverId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.BlockedUsers", "BlockedUserId", "dbo.Users");
            DropForeignKey("dbo.BlockedUsers", "BlockerUserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.BlockedUsers", new[] { "BlockedUserId" });
            DropIndex("dbo.BlockedUsers", new[] { "BlockerUserId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.BlockedUsers");
        }
    }
}
