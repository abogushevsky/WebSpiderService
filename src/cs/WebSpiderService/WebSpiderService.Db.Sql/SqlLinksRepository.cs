using System;
using System.Diagnostics.Contracts;
using System.Linq;
using WebSpiderService.Common.Entities;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Db.Sql
{
    public class SqlLinksRepository : ILinksRepository
    {
        public Guid SaveLink(Common.Entities.Link link)
        {
            Contract.Requires(link != null);
            Contract.Requires(link.LinkContentType != null);
            Contract.Requires(!string.IsNullOrEmpty(link.LinkContentType.ContentType));

            if (link.Id == Guid.Empty)
            {
                link.Id = Guid.NewGuid();
                link.CreatedDate = DateTime.Now;
            }

            lock (this)
            {
                using (WebSpiderDbContext context = new WebSpiderDbContext())
                {
                    LinkContentType contentType =
                        context.LinkContentTypes.SingleOrDefault(
                            ct =>
                                ct.ContentType.Equals(link.LinkContentType.ContentType,
                                    StringComparison.InvariantCultureIgnoreCase));

                    if (contentType == null)
                    {
                        contentType = new LinkContentType()
                        {
                            Id = Guid.NewGuid(),
                            ContentType = link.LinkContentType.ContentType,
                            FileExtension = link.LinkContentType.FileExtension
                        };
                    }

                    link.LinkContentType = contentType;
                    link.LinkContentTypeId = contentType.Id;

                    context.Links.Add(link);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return Guid.Empty;
                    }
                }
            }

            return link.Id;
        }

    }
}
