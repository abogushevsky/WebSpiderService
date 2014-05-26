using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;
using WebSpiderService.Common.Entities;
using WebSpiderService.Common.Interfaces;
using WebSpiderService.Common.SocialNetworks;
using WebSpiderService.Db.Mongo;
using WebSpiderService.Impl;

namespace WebSpiderService.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            ISpiderService spiderService = context.GetObject("WebSpiderService") as ISpiderService;
            IDocumentsRepository documentsRepository = context.GetObject("MongoDbDocumentsRepository") as IDocumentsRepository;
            IDocumentAnalizer documentAnalizer = context.GetObject("RegexDocumentAnalizer") as IDocumentAnalizer;

            if (spiderService == null)
            {
                System.Console.WriteLine("Spider service is not configured!");
                return;
            }

            System.Console.WriteLine("Press any key to start downloading");
            System.Console.ReadKey();
            System.Console.WriteLine("Downloading...");

            spiderService.ClearRepositories();
            spiderService.DowloadDocuments();

            Document[] remainingDocuments = documentsRepository.GetAllDocuments();
            Parallel.ForEach(remainingDocuments, document =>
            {
                string[] urls = documentAnalizer.GetLinksFromDocument(document);
                spiderService.DownloadDocuments(urls);
            });

            System.Console.WriteLine("Done! Press any ket to stop application");
            System.Console.ReadLine();
        }
    }
}
