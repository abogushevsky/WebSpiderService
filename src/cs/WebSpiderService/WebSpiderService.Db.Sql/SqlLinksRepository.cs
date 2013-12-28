using System.Diagnostics.Contracts;
using System.Linq;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Db.Sql
{
    public class SqlLinksRepository : ILinksRepository
    {
        public int SaveLink(Common.Entities.Link link)
        {
            Contract.Requires(link != null);
            Contract.Requires(link.LinkContentType != null);
            Contract.Requires(!string.IsNullOrEmpty(link.LinkContentType.ContentType));
            lock (this)
            {
                using (WebSpiderDBEntities context = new WebSpiderDBEntities())
                {
                    ContentType contentType =
                        context.ContentTypes.SingleOrDefault(
                            ct => ct.LinkContentType == link.LinkContentType.ContentType);
                    if (contentType == null)
                    {
                        contentType = new ContentType()
                        {
                            LinkContentType = link.LinkContentType.ContentType
                        };
                        context.ContentTypes.Add(contentType);
                    }

                    Link newLink = new Link()
                    {
                        ContentType = contentType,
                        ContentTypeId = contentType.Id,
                        ParentId = link.ParentId,
                        Url = link.Url
                    };

                    context.Links.Add(newLink);
                    context.SaveChanges();

                    return newLink.Id;
                }
            }
        }
    }
}