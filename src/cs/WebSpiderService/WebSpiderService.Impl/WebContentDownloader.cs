using System;
using System.Net;
using System.Threading.Tasks;
using WebSpiderService.Common.Entities;
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
        public async Task<LinkResult> DownloadUrlAsync(string url)
        {
            throw new NotImplementedException();
            //WebClient webClient = new WebClient();
            //return await webClient.DownloadStringTaskAsync(new Uri(url));
        }

        /// <summary>
        /// Method asynchronously downloads site resourse by full or relative path
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public async Task<LinkResult> DownloadSiteResourseAsync(string siteUrl, string resourcePath)
        {
            throw new NotImplementedException();

            //if (!resourcePath.StartsWith("/"))
            //{
            //    return await DownloadUrlAsync(resourcePath);
            //}

            //try
            //{
            //    WebClient webClient = new WebClient();
            //    Uri baseUri = new Uri(siteUrl);
            //    Uri uri = new Uri(baseUri, resourcePath);
            //    return await webClient.DownloadStringTaskAsync(uri);
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Method dowloads page content
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public LinkResult DownloadUrl(string url)
        {
            LinkResult result = new LinkResult();
            result.Link = new Link();
            result.Link.Url = url;

            try
            {
                WebClient webClient = new WebClient();
                Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
                result.Content = webClient.DownloadString(uri);
                result.Link.LinkContentType = new LinkContentType()
                {
                    ContentType = webClient.ResponseHeaders.Get("Content-Type")
                };
            }
            catch
            {
                //log
            }

            return result;
        }

        /// <summary>
        /// Method downloads site resourse by full or relative path
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public LinkResult DownloadSiteResourse(string siteUrl, string resourcePath)
        {
            if (!resourcePath.StartsWith("/"))
            {
                return DownloadUrl(resourcePath);
            }

            LinkResult result = new LinkResult();
            result.Link = new Link();

            try
            {
                WebClient webClient = new WebClient();
                Uri baseUri = new Uri(siteUrl);
                Uri uri = new Uri(baseUri, resourcePath);
                result.Link.Url = uri.ToString();
                result.Content = webClient.DownloadString(uri);
                result.Link.LinkContentType = new LinkContentType()
                {
                    ContentType = webClient.ResponseHeaders.Get("Content-Type")
                };
            }
            catch
            {
               
            }

            return result;
        }
    }
}