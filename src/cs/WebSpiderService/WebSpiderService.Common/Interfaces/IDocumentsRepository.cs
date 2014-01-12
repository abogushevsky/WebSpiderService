using System;
using System.Security.Cryptography.X509Certificates;
using WebSpiderService.Common.Entities;

namespace WebSpiderService.Common.Interfaces
{
    /// <summary>
    /// Interface describes document repositiry
    /// </summary>
    public interface IDocumentsRepository
    {
        /// <summary>
        /// Adds new document to repository
        /// </summary>
        /// <param name="document"></param>
        void StoreDocument(Document document);

        /// <summary>
        /// Returns document by its linkId
        /// </summary>
        /// <returns></returns>
        Document GetDocumentByLinkId(Guid linkId);

        /// <summary>
        /// Returns document by its own id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Document GetDocumentById(object id);

        /// <summary>
        /// Returns all document found in repository
        /// </summary>
        /// <returns></returns>
        Document[] GetAllDocuments();

        /// <summary>
        /// Removes all Documents from storage
        /// </summary>
        void RemoveAllDocuments();
    }
}