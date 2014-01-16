using System.ComponentModel;

namespace WebSpiderService.Db.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shouldDownload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LinkContentTypes", "ShouldDownload", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LinkContentTypes", "ShouldDownload");
        }
    }
}
