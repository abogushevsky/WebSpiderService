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
        public async Task<string> DownloadSiteResourseAsync(string siteUrl, string resourcePath)
        {
            if (!resourcePath.StartsWith("/"))
            {
                return await DownloadUrlAsync(resourcePath);
            }

            try
            {
                WebClient webClient = new WebClient();
                Uri baseUri = new Uri(siteUrl);
                Uri uri = new Uri(baseUri, resourcePath);
                return await webClient.DownloadStringTaskAsync(uri);
            }
            catch
            {
                return null;
            }
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
            string link = resourcePath.Replace("href=\"", "").Replace("\"", "");

            if (!link.StartsWith("/"))
            {
                return DownloadUrl(resourcePath);
            }

            try
            {
                WebClient webClient = new WebClient();
                Uri baseUri = new Uri(siteUrl);
                Uri uri = new Uri(baseUri, link);
                return webClient.DownloadString(uri);
            }
            catch
            {
                return null;
            }
        }
    }
}