using System;
using System.Net;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Impl
{
    /// <summary>
    /// Implementation of IContentDownloader that uses WebClient to download web pages
    /// content from Web
    /// </summary>
    public class WebContentDownloader : IContentDownloader
    {
        /// <summary>
        /// Method asynchronously dowloads page content
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> DownloadUrlAsync(string url)
        {
            WebClient webClient = new WebClient();
            return await webClient.DownloadStringTaskAsync(new Uri(url));
        }

        /// <summary>
        /// Method asynchronously downloads site resourse by full or relative path
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public Task<string> DownloadSiteResourseAsync(string siteUrl, string resourcePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method dowloads page content
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DownloadUrl(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
                return webClient.DownloadString(uri);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Method downloads site resourse by full or relative path
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public string DownloadSiteResourse(string siteUrl, string resourcePath)
        {
            throw new NotImplementedException();
        }
    }
}