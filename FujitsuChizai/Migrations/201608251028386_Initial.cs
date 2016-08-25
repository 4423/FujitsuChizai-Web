namespace FujitsuChizai.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Edges",
                c => new
                    {
                        PlaceMarkId1 = c.Int(nullable: false),
                        PlaceMarkId2 = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        PlaceMark_Id = c.Int(),
                        PlaceMark1_Id = c.Int(),
                        PlaceMark2_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.PlaceMarkId1, t.PlaceMarkId2 })
                .ForeignKey("dbo.PlaceMarks", t => t.PlaceMark_Id)
                .ForeignKey("dbo.PlaceMarks", t => t.PlaceMark1_Id)
                .ForeignKey("dbo.PlaceMarks", t => t.PlaceMark2_Id)
                .Index(t => t.PlaceMark_Id)
                .Index(t => t.PlaceMark1_Id)
                .Index(t => t.PlaceMark2_Id);
            
            CreateTable(
                "dbo.PlaceMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        LightId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        PlaceMarkId_Id = c.Int(),
                        UserId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaceMarks", t => t.PlaceMarkId_Id)
                .ForeignKey("dbo.Users", t => t.UserId_Id)
                .Index(t => t.PlaceMarkId_Id)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BornIn = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Floor = c.Int(nullable: false),
                        Description = c.String(),
                        MapImageFilePath = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Edges", "PlaceMark2_Id", "dbo.PlaceMarks");
            DropForeignKey("dbo.Edges", "PlaceMark1_Id", "dbo.PlaceMarks");
            DropForeignKey("dbo.Histories", "UserId_Id", "dbo.Users");
            DropForeignKey("dbo.Histories", "PlaceMarkId_Id", "dbo.PlaceMarks");
            DropForeignKey("dbo.Edges", "PlaceMark_Id", "dbo.PlaceMarks");
            DropIndex("dbo.Histories", new[] { "UserId_Id" });
            DropIndex("dbo.Histories", new[] { "PlaceMarkId_Id" });
            DropIndex("dbo.Edges", new[] { "PlaceMark2_Id" });
            DropIndex("dbo.Edges", new[] { "PlaceMark1_Id" });
            DropIndex("dbo.Edges", new[] { "PlaceMark_Id" });
            DropTable("dbo.Maps");
            DropTable("dbo.Users");
            DropTable("dbo.Histories");
            DropTable("dbo.PlaceMarks");
            DropTable("dbo.Edges");
        }
    }
}
