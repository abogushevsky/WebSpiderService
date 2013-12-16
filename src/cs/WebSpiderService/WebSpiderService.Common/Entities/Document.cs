namespace WebSpiderService.Common.Entities
{
    /// <summary>
    /// Class represents a web document
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Url of the document
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Document content
        /// </summary>
        public string Content { get; set; }
    }
}