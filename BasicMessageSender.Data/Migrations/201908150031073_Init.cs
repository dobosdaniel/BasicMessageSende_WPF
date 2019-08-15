namespace BasicMessageSender.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                        Receiver_Id = c.Int(),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_Id)
                .ForeignKey("dbo.Users", t => t.Sender_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Users");
            DropForeignKey("dbo.BlockedUsers", "BlockedUserId", "dbo.Users");
            DropForeignKey("dbo.BlockedUsers", "BlockerUserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.BlockedUsers", new[] { "BlockedUserId" });
            DropIndex("dbo.BlockedUsers", new[] { "BlockerUserId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.BlockedUsers");
        }
    }
}
