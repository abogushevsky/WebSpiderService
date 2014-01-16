using System;
using WebSpiderService.Common.Entities;

namespace WebSpiderService.Common.Interfaces
{
    /// <summary>
    /// Interface describes html document analizer
    /// </summary>
    public interface IDocumentAnalizer
    {
        /// <summary>
        /// Method finds and returns urls in document content
        /// </summary>
        string[] GetLinksFromDocument(Document document);
    }
}