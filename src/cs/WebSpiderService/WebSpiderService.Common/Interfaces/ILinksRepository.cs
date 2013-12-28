using WebSpiderService.Common.Entities;

namespace WebSpiderService.Common.Interfaces
{
    public interface ILinksRepository
    {
        int SaveLink(Link link);
    }
}