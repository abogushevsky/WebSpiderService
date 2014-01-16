namespace WebSpiderService.Db.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class link_last_downloaded_date_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "LastDownloadedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "LastDownloadedDate");
        }
    }
}
