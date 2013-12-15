using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Impl
{
    /// <summary>
    /// Simple implementation of ISpiderService using file with urls list
    /// </summary>
    public class SimpleWebSpider : ISpiderService
    {
        private long _currentDocumentIndex;
        private readonly IContentDownloader _contentDownloader;
        private readonly IDocumentAnalizer _documentAnalizer;

        public SimpleWebSpider(IContentDownloader contentDownloader, IDocumentAnalizer documentAnalizer)
        {
            Contract.Requires(contentDownloader != null);
            Contract.Requires(documentAnalizer != null);

            this._contentDownloader = contentDownloader;
            this._documentAnalizer = documentAnalizer;
            this._currentDocumentIndex = 0;
        }

        /// <summary>
        /// Method is intended to download all documents
        /// </summary>
        public void DowloadDocuments()
        {
            DownloadDocuments(GetUrls());

            string[] documentsFileNames = Directory.GetFiles(Properties.Settings.Default.DocumentsFolderPath);
            //Parallel.ForEach(documentsFileNames, (docFileName) =>
            //{
            //    string documentContent = GetDocumentContentFromFile(docFileName);
            //    string[] documentUrls = this._documentAnalizer.GetLinksFromDocument(documentContent);
            //    DownloadDocuments(documentUrls);
            //});

            foreach (string docFileName in documentsFileNames)
            {
                string documentContent = GetDocumentContentFromFile(docFileName);
                string[] documentUrls = this._documentAnalizer.GetLinksFromDocument(documentContent);
                DownloadDocuments(documentUrls);   
            }
        }

        private void DownloadDocuments(string[] urlsToDownload)
        {
            Parallel.ForEach(urlsToDownload, (url) =>
            {
                string document = this._contentDownloader.DownloadUrl(url);
                if (!string.IsNullOrEmpty(document))
                {
                    SaveDocument(url, document);
                }
            });
        }

        private string GetDocumentContentFromFile(string docFileName)
        {
            using (FileStream fs = new FileStream(docFileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private string[] GetUrls()
        {
            List<string> result = new List<string>();

            using (FileStream fs = new FileStream(Properties.Settings.Default.TargetsFileFullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        //TODO: Add url validation
                        if (!string.IsNullOrEmpty(line))
                        {
                            result.Add(line);
                        }
                    }
                }
            }

            return result.ToArray();
        }

        private void SaveDocument(string url, string document)
        {
            string documentFilePath = string.Format("{0}\\{1}.txt", Properties.Settings.Default.DocumentsFolderPath,
                url.Replace("/", "").Replace(":", ""));

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
