using System;
using MongoDB.Bson;

namespace WebSpiderService.Common.Entities
{
    /// <summary>
    /// Class represents a web document
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Document Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Url of the document
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Link id from links storage
        /// </summary>
        public Guid LinkId { get; set; }

        /// <summary>
        /// Document content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Document download date
        /// </summary>
        public DateTime DownloadDate { get; set; }
    }
}