using System;
using System.Threading.Tasks;
using WebSpiderService.Common.SocialNetworks.Entities;

namespace WebSpiderService.Common.SocialNetworks.Interfaces
{
    public interface ISocialContentDownloader
    {
        Task<Person> GetPersonData(string id);

        Task<FeedItem[]> GetPersonFeed(string personId, string lastFeedItemId);
    }
}