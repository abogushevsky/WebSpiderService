using System;

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
        /// Link id from data storage
        /// </summary>
        public Guid LinkId { get; set; }

        /// <summary>
        /// Document content
        /// </summary>
        public string Content { get; set; }
    }
}