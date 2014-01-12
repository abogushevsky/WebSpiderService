namespace WebSpiderService.Db.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LinkContentTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentType = c.String(nullable: false, maxLength: 128),
                        FileExtension = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Url = c.String(nullable: false, maxLength: 512),
                        CreatedDate = c.DateTime(nullable: false),
                        LinkContentTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LinkContentTypes", t => t.LinkContentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Links", t => t.ParentId)
                .Index(t => t.LinkContentTypeId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "ParentId", "dbo.Links");
            DropForeignKey("dbo.Links", "LinkContentTypeId", "dbo.LinkContentTypes");
            DropIndex("dbo.Links", new[] { "ParentId" });
            DropIndex("dbo.Links", new[] { "LinkContentTypeId" });
            DropTable("dbo.Links");
            DropTable("dbo.LinkContentTypes");
        }
    }
}
