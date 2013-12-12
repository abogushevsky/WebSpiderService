using System.Net;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderServiceImpl
{
    public class WebContentDownloader : IContentDownloader
    {
        public async Task<string> DownloadUrlAsync(string url)
        {
            WebClient webClient = new WebClient();
            return await webClient.DownloadStringTaskAsync(url);
        }

        string IContentDownloader.DownloadUrl(string url)
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString(url);
        }
    }
}