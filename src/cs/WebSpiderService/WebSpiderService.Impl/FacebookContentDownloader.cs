using System;
using System.Net;
using System.Threading.Tasks;
using WebSpiderService.Common.SocialNetworks;

namespace WebSpiderService.Impl
{
    public class FacebookContentDownloader : ISocialContentDownloader
    {
        private const string AUTH_URL_TEMPLATE =
            "https://graph.facebook.com//oauth/access_token?client_id={0}&client_secret={1}&grant_type=client_credentials";

        private string currentToken = null;
        private string appId = "1382955545297564";
        private string clientSecret = "17682db45d67d34fd0f58cc37dba0d66";

        public FacebookContentDownloader()
        {
            GetAuthToken().Wait();
        }

        private async Task GetAuthToken()
        {
            using (WebClient webClient = new WebClient())
            {
                string result = await webClient.DownloadStringTaskAsync(string.Format(AUTH_URL_TEMPLATE, this.appId, this.clientSecret));
                Console.WriteLine(result);
            }
        }
    }
}