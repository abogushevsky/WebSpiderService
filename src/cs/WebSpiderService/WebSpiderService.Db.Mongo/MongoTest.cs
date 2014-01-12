using System;
using MongoDB.Driver;
using WebSpiderService.Common.Entities;

namespace WebSpiderService.Db.Mongo
{
    public class MongoTest
    {
        public static void Test()
        {
            string connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("WebSpiderDb");

            Document doc = new Document()
            {
                Content = "blabla bla bla 111 ",
                DownloadDate = DateTime.Now,
                Url = "www.blabla111.com",
                LinkId = Guid.NewGuid()
            };

            MongoCollection<Document> collection = database.GetCollection<Document>("documents");
            collection.Insert(doc);
        }

        public static void TestGet()
        {
            string connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("WebSpiderDb");

            MongoCollection<Document> collection = database.GetCollection<Document>("documents");
            MongoCursor<Document> cursor = collection.FindAll();
            foreach (Document document in cursor)
            {
                Console.WriteLine(document.Url);
            }
        }
    }
}