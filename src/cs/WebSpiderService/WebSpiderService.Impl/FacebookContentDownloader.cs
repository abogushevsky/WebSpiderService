using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSpiderService.Common.SocialNetworks;
using WebSpiderService.Common.SocialNetworks.Entities;
using WebSpiderService.Common.SocialNetworks.Interfaces;

namespace WebSpiderService.Impl
{
    public class FacebookContentDownloader : ISocialContentDownloader
    {
        //1346150481/posts?limit=25&since=1369515451 - paging

        private const string AUTH_URL_TEMPLATE =
            "https://graph.facebook.com//oauth/access_token?client_id={0}&client_secret={1}&grant_type=client_credentials";

        private const string PERSON_INFO_URL_TEMPLATE = "https://graph.facebook.com/{0}?{1}"; 

        private string currentToken = null;
        private string appId = "1382955545297564";
        private string clientSecret = "17682db45d67d34fd0f58cc37dba0d66";

        public FacebookContentDownloader()
        {
            
        }

        private async Task GetAuthToken()
        {
            using (WebClient webClient = new WebClient())
            {
                this.currentToken = await webClient.DownloadStringTaskAsync(string.Format(AUTH_URL_TEMPLATE, this.appId, this.clientSecret));
            }
        }

        public async Task<Person> GetPersonData(string id)
        {
            if (currentToken == null)
            {
                await GetAuthToken();
            }

            using (WebClient webClient = new WebClient())
            {
                string personData =
                    await
                        webClient.DownloadStringTaskAsync(string.Format(PERSON_INFO_URL_TEMPLATE, id, this.currentToken));

                JObject obj = JObject.Parse(personData);

                Console.WriteLine(personData);
            }

            return null;
        }

        public async Task<FeedItem[]> GetPersonFeed(string personId, string lastFeedItemId)
        {
            if (currentToken == null)
            {
                await GetAuthToken();
            }

            using (WebClient webClient = new WebClient())
            {

            }

            return null;
        }
    }
}