namespace FujitsuChizai.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDestHistory2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Histories", new[] { "OriginId" });
            DropIndex("dbo.Histories", new[] { "DestId" });
            AlterColumn("dbo.Histories", "OriginId", c => c.Int(nullable: false));
            AlterColumn("dbo.Histories", "DestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Histories", "OriginId");
            CreateIndex("dbo.Histories", "DestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Histories", new[] { "DestId" });
            DropIndex("dbo.Histories", new[] { "OriginId" });
            AlterColumn("dbo.Histories", "DestId", c => c.Int());
            AlterColumn("dbo.Histories", "OriginId", c => c.Int());
            CreateIndex("dbo.Histories", "DestId");
            CreateIndex("dbo.Histories", "OriginId");
        }
    }
}
