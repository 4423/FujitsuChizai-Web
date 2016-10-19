namespace FujitsuChizai.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDestHistory : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Histories", new[] { "PlaceMarkId" });
            RenameColumn(table: "dbo.Histories", name: "PlaceMarkId", newName: "PlaceMark_Id");
            AddColumn("dbo.Histories", "OriginId", c => c.Int());
            AddColumn("dbo.Histories", "DestId", c => c.Int());
            AlterColumn("dbo.Histories", "PlaceMark_Id", c => c.Int());
            CreateIndex("dbo.Histories", "OriginId");
            CreateIndex("dbo.Histories", "DestId");
            CreateIndex("dbo.Histories", "PlaceMark_Id");
            AddForeignKey("dbo.Histories", "DestId", "dbo.PlaceMarks", "Id");
            AddForeignKey("dbo.Histories", "OriginId", "dbo.PlaceMarks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "OriginId", "dbo.PlaceMarks");
            DropForeignKey("dbo.Histories", "DestId", "dbo.PlaceMarks");
            DropIndex("dbo.Histories", new[] { "PlaceMark_Id" });
            DropIndex("dbo.Histories", new[] { "DestId" });
            DropIndex("dbo.Histories", new[] { "OriginId" });
            AlterColumn("dbo.Histories", "PlaceMark_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Histories", "DestId");
            DropColumn("dbo.Histories", "OriginId");
            RenameColumn(table: "dbo.Histories", name: "PlaceMark_Id", newName: "PlaceMarkId");
            CreateIndex("dbo.Histories", "PlaceMarkId");
        }
    }
}
