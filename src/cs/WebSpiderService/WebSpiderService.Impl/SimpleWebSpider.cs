using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpiderService.Common.Entities;
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

            Parallel.ForEach(documentsFileNames, (docFileName) =>
            {
                Document parentDocument = GetDocumentContentFromFile(docFileName);
                string[] documentUrls = this._documentAnalizer.GetLinksFromDocument(parentDocument.Content);
                string linksFolder = docFileName.Replace(".txt", "");
                linksFolder = linksFolder.Substring(linksFolder.LastIndexOf('\\'));
                
                Parallel.For(0, documentUrls.Length, (i) =>
                {
                    string document = this._contentDownloader.DownloadSiteResourse(parentDocument.Url, documentUrls[i]);
                    if (document != null)
                    {
                        SaveDocument(documentUrls[i], document, linksFolder, i.ToString());
                    }
                });
            });
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

        private Document GetDocumentContentFromFile(string docFileName)
        {
            Document result = new Document();

            using (FileStream fs = new FileStream(docFileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    result.Url = reader.ReadLine();
                    result.Content = reader.ReadToEnd();
                }
            }

            return result;
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

        private void SaveDocument(string url, string document, string subfolderName = null, string fileName = null)
        {
            string filePath = !string.IsNullOrEmpty(subfolderName) ? 
                string.Format("{0}\\{1}", Properties.Settings.Default.DocumentsFolderPath, subfolderName) : 
                Properties.Settings.Default.DocumentsFolderPath;

            string documentFilePath = string.Format("{0}\\{1}.txt", filePath,
                !string.IsNullOrEmpty(fileName) ? fileName : RemoveInvalidCharacters(url));

            StringBuilder fileContentBuilder = new StringBuilder();
            fileContentBuilder.AppendLine(url);
            fileContentBuilder.Append(document);
            SaveContentToFile(documentFilePath, fileContentBuilder.ToString());
        }

        private void SaveContentToFile(string fileName, string content)
        {
            string directory = fileName.Substring(0, fileName.LastIndexOf('\\'));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(content);
                }
            }
        }

        private string RemoveInvalidCharacters(string source)
        {
            return source.Replace("/", "").Replace(":", "");
        }
    }
}
