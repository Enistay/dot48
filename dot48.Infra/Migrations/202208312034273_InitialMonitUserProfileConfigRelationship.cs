namespace dot48.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialdot48UserProfileConfigRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        IdProfile = c.Int(nullable: false, identity: true),
                        CodeProfile = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.IdProfile);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        AliasName = c.String(maxLength: 150),
                        Nif = c.String(maxLength: 20),
                        CodeUser = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        PhoneNumber = c.String(maxLength: 14),
                        Password = c.String(nullable: false, maxLength: 64),
                        Enable = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        UserSettings_Id = c.Int(),
                    })
                .PrimaryKey(t => t.IdUser)
                .ForeignKey("dbo.UserSetting", t => t.UserSettings_Id)
                .Index(t => t.CodeUser, unique: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.UserSettings_Id);
            
            CreateTable(
                "dbo.UserSetting",
                c => new
                    {
                        IdUserSetting = c.Int(nullable: false, identity: true),
                        MO = c.Boolean(),
                        RecipientMO = c.Boolean(),
                        DIR = c.Boolean(),
                        RecipientDIR = c.Boolean(),
                        WorkHours = c.Short(),
                    })
                .PrimaryKey(t => t.IdUserSetting);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Profile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Profile_Id })
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Profile", t => t.Profile_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Profile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserSettings_Id", "dbo.UserSetting");
            DropForeignKey("dbo.UserProfile", "Profile_Id", "dbo.Profile");
            DropForeignKey("dbo.UserProfile", "User_Id", "dbo.User");
            DropIndex("dbo.UserProfile", new[] { "Profile_Id" });
            DropIndex("dbo.UserProfile", new[] { "User_Id" });
            DropIndex("dbo.User", new[] { "UserSettings_Id" });
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.User", new[] { "CodeUser" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.UserSetting");
            DropTable("dbo.User");
            DropTable("dbo.Profile");
        }
    }
}
