using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;
using WebSpiderService.Common.Interfaces;
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

            System.Console.WriteLine("Done! Press any ket to stop application");
            System.Console.ReadLine();
        }
    }
}
