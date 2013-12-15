using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;
using WebSpiderService.Impl;

namespace WebSpiderService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpiderService spiderService = new SimpleWebSpider(new WebContentDownloader(), new RegexDocumentAnalizer());

            System.Console.WriteLine("Press any key to start downloading");
            System.Console.ReadKey();
            System.Console.WriteLine("Downloading...");

            spiderService.DowloadDocuments();

            System.Console.WriteLine("Done!");
        }
    }
}
