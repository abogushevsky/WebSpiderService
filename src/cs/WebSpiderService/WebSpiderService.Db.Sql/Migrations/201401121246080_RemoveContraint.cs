namespace WebSpiderService.Db.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveContraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LinkContentTypes", "FileExtension", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LinkContentTypes", "FileExtension", c => c.String(nullable: false, maxLength: 16));
        }
    }
}
