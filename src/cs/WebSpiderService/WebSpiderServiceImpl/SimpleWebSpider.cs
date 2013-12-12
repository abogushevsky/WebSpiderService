using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderServiceImpl
{
    /// <summary>
    /// Simple implementation of ISpiderService using file with urls list
    /// </summary>
    public class SimpleWebSpider : ISpiderService
    {
        private long _currentDocumentIndex;
        private readonly IContentDownloader _contentDownloader;

        public SimpleWebSpider(IContentDownloader contentDownloader)
        {
            Contract.Requires(contentDownloader != null);

            this._contentDownloader = contentDownloader;
            this._currentDocumentIndex = 0;
        }

        /// <summary>
        /// Method is intended to download all documents
        /// </summary>
        public void DowloadDocuments()
        {
            string[] urlsToDownload = GetUrls();
            Parallel.ForEach(urlsToDownload, (url) =>
            {
                string document = this._contentDownloader.DownloadUrl(url);
                SaveDocument(url, document);
            });
        }

        private string[] GetUrls()
        {
            List<string> result = new List<string>();

            using (FileStream fs = new FileStream(Properties.Settings.Default.TargetsFileFullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line = reader.ReadLine();
                    //TODO: Add url validation
                    if (!string.IsNullOrEmpty(line))
                    {
                        result.Add(line);
                    }
                }
            }

            return result.ToArray();
        }

        private void SaveDocument(string url, string document)
        {
            string documentFilePath = string.Format("{0}\\{1}", Properties.Settings.Default.DocumentsFolderPath,
                this._currentDocumentIndex);
            using (FileStream fs = new FileStream(documentFilePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(url);
                    writer.Write(document);
                }
            }
        }
    }
}
