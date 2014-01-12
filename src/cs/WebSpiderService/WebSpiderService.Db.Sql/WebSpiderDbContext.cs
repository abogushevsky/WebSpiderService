using System.Data.Entity;
using WebSpiderService.Common.Entities;

namespace WebSpiderService.Db.Sql
{
    public class WebSpiderDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public DbSet<LinkContentType> LinkContentTypes { get; set; }
    }
}