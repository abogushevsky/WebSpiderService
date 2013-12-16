using System.Threading.Tasks;

namespace WebSpiderService.Common.Interfaces
{
    /// <summary>
    /// Interface provides methods to download content of web pages
    /// </summary>
    public interface IContentDownloader
    {
        /// <summary>
        /// Method dowloads page content
        /// </summary>
        /// <param name="url">Page url</param>
        /// <returns>Page content</returns>
        string DownloadUrl(string url);

        /// <summary>
        /// Method downloads site resourse by full or relative path
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        string DownloadSiteResourse(string siteUrl, string resourcePath);

        /// <summary>
        /// Method asynchronously dowloads page content
        /// </summary>
        /// <param name="url">Page url</param>
        /// <returns>Page content</returns>
        Task<string> DownloadUrlAsync(string url);

        /// <summary>
        /// Method asynchronously downloads site resourse by full or relative path
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        Task<string> DownloadSiteResourseAsync(string siteUrl, string resourcePath);
    }
}