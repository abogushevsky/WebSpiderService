using System;
using WebSpiderService.Common.Entities;

namespace WebSpiderService.Common.Interfaces
{
    public interface ILinksRepository
    {
        Guid SaveLink(Link link);
    }
}