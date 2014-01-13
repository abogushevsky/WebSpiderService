using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.SqlServer.Server;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using WebSpiderService.Common.Entities;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Db.Mongo
{
    /// <summary>
    /// Implementation of IDocumentsRepository working with MongoDb database engine
    /// </summary>
    public class MongoDbDocumentsRepository : IDocumentsRepository
    {
        private const string DB_NAME = "WebSpiderDb";

        private readonly string _connectionString;
        private readonly MongoClient _client;
        private readonly MongoServer _server;
        private readonly MongoDatabase _db;
        private readonly MongoCollection<Document> _documentsCollection;

        public MongoDbDocumentsRepository(string connectionString)
        {
            Contract.Requires(connectionString != null);

            this._connectionString = connectionString;
            this._client = new MongoClient(connectionString);
            this._server = this._client.GetServer();
            this._db = this._server.GetDatabase(DB_NAME);
            this._documentsCollection = this._db.GetCollection<Document>("documents");

        }

        /// <summary>
        /// Adds new document to repository
        /// </summary>
        /// <param name="document"></param>
        public void StoreDocument(Document document)
        {
            //MongoDB.Driver.GridFS.MongoGridFS fs = new MongoGridFS(this._server, this._db.Name, new MongoGridFSSettings());
            try
            {
                this._documentsCollection.Insert(document);
            }
            catch (Exception ex)
            {
                //TODO: log exception
            }
        }

        /// <summary>
        /// Returns document by its linkId
        /// </summary>
        /// <returns></returns>
        public Document GetDocumentByLinkId(Guid linkId)
        {
            IMongoQuery query = Query<Document>.EQ(e => e.LinkId, linkId);
            Document result = this._documentsCollection.FindOne(query);
            return result;
        }

        /// <summary>
        /// Returns document by its own id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Document GetDocumentById(object id)
        {
            throw new NotImplementedException();
            //IMongoQuery query = Query<Document>.EQ(e => e.Id, id);
            //Document result = this._documentsCollection.FindOne(query);
            //return result;
        }

        /// <summary>
        /// Returns all document found in repository
        /// </summary>
        /// <returns></returns>
        public Document[] GetAllDocuments()
        {
            MongoCursor<Document> result = this._documentsCollection.FindAll();
            return result.ToArray();
        }

        /// <summary>
        /// Removes all Documents from storage
        /// </summary>
        public void RemoveAllDocuments()
        {
            this._documentsCollection.RemoveAll();
        }
    }
}