using System;
using System.Diagnostics.Contracts;
using System.Linq;
using WebSpiderService.Common.Entities;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Db.Sql
{
    public class SqlLinksRepository : ILinksRepository
    {
        private static object _locker = new object();

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

            lock (_locker)
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

                    Link existingLink =
                        context.Links.SingleOrDefault(
                            lnk => lnk.Url.Equals(link.Url, StringComparison.InvariantCultureIgnoreCase));

                    if (existingLink != null)
                    {
                        existingLink.LastDownloadedDate = link.LastDownloadedDate;
                        existingLink.LinkContentType = link.LinkContentType;
                        existingLink.LinkContentTypeId = link.LinkContentTypeId;
                        link.Id = existingLink.Id;
                    }
                    else
                    {
                        context.Links.Add(link);
                    }

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

        public Link[] GetAllLinks()
        {
            using (WebSpiderDbContext context = new WebSpiderDbContext())
            {
                return context.Links.ToArray();
            }
        }

    }
}
