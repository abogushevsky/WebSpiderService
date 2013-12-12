using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;
using WebSpiderServiceImpl;

namespace WebSpiderService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpiderService spiderService = new SimpleWebSpider(new WebContentDownloader());
            spiderService.DowloadDocuments();

            System.Console.WriteLine("Done!");
        }
    }
}
