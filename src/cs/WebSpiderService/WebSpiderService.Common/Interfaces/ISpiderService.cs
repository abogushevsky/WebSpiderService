namespace WebSpiderService.Common.Interfaces
{
    /// <summary>
    /// Interface describes a Web Spider
    /// </summary>
    public interface ISpiderService
    {
        /// <summary>
        /// Method is intended to download all documents
        /// </summary>
        void DowloadDocuments();

        /// <summary>
        /// Method is intended to download all documents from specified urls
        /// </summary>
        /// <param name="urls">Urls to download</param>
        void DownloadDocuments(string[] urls);

        /// <summary>
        /// Clears all stored data collected by the spider
        /// </summary>
        void ClearRepositories();
    }
}