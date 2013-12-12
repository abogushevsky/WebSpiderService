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
        /// Method asynchronously dowloads page content
        /// </summary>
        /// <param name="url">Page url</param>
        /// <returns>Page content</returns>
        Task<string> DownloadUrlAsync(string url);
    }
}