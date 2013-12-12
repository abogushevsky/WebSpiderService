using System;
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
            return await webClient.DownloadStringTaskAsync(new Uri(url));
        }

        string IContentDownloader.DownloadUrl(string url)
        {
            WebClient webClient = new WebClient();
            Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);

            try
            {
                return webClient.DownloadString(uri);
            }
            catch
            {
                return null;
            }
        }
    }
}