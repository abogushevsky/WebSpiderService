using System;
using System.Collections.Concurrent;
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
        private readonly IContentDownloader _contentDownloader;
        private readonly IDocumentAnalizer _documentAnalizer;
        private readonly ILinksRepository _linksRepository;
        private readonly IDocumentsRepository _documentsRepository;

        public SimpleWebSpider(IContentDownloader contentDownloader, IDocumentAnalizer documentAnalizer, ILinksRepository linksRepository, IDocumentsRepository documentsRepository)
        {
            Contract.Requires(contentDownloader != null);
            Contract.Requires(documentAnalizer != null);
            Contract.Requires(linksRepository != null);
            Contract.Requires(documentsRepository != null);

            this._contentDownloader = contentDownloader;
            this._documentAnalizer = documentAnalizer;
            this._linksRepository = linksRepository;
            this._documentsRepository = documentsRepository;
        }

        /// <summary>
        /// Method is intended to download all documents
        /// </summary>
        public void DowloadDocuments()
        {
            Link[] linksToDownload = this._linksRepository.GetAllLinks();
            DownloadDocuments(linksToDownload.Select(lnk => lnk.Url).ToArray());

            string[] documentsFileNames = Directory.GetFiles(Properties.Settings.Default.DocumentsFolderPath);

            Parallel.ForEach(documentsFileNames, (docFileName) =>
            {
                Document parentDocument = GetDocumentContentFromFile(docFileName);
                string[] documentUrls = this._documentAnalizer.GetLinksFromDocument(parentDocument);
                string linksFolder = docFileName.Replace(".txt", "");
                linksFolder = linksFolder.Substring(linksFolder.LastIndexOf('\\'));
                
                Parallel.For(0, documentUrls.Length, (i) =>
                {
                    LinkResult linkDownloadResult = this._contentDownloader.DownloadSiteResourse(parentDocument.Url, documentUrls[i]);
                    if (linkDownloadResult != null)
                    {
                        if (linkDownloadResult.Link != null && linkDownloadResult.Link.LinkContentType != null)
                        {
                            linkDownloadResult.Link.ParentId = parentDocument.LinkId;
                            linkDownloadResult.Link.LastDownloadedDate = DateTime.Now;
                            this._linksRepository.SaveLink(linkDownloadResult.Link);
                            this._documentsRepository.StoreDocument(new Document()
                            {
                                Content = linkDownloadResult.Content,
                                DownloadDate = DateTime.Now,
                                LinkId = linkDownloadResult.Link.Id,
                                Url = linkDownloadResult.Link.Url
                            });
                        }
                    }
                });
            });
        }

        /// <summary>
        /// Method is intended to download all documents from specified urls
        /// </summary>
        /// <param name="urls">Urls to download</param>
        public void DownloadDocuments(string[] urls)
        {
            Parallel.ForEach(urls, (url) =>
            {
                LinkResult linkDownloadResult = this._contentDownloader.DownloadUrl(url);
                if (linkDownloadResult.Content != null)
                {
                    Guid linkId = Guid.Empty;
                    if (linkDownloadResult.Link != null && linkDownloadResult.Link.LinkContentType != null)
                    {
                        linkId = this._linksRepository.SaveLink(linkDownloadResult.Link);
                        this._documentsRepository.StoreDocument(new Document()
                        {
                            Content = linkDownloadResult.Content,
                            DownloadDate = DateTime.Now,
                            LinkId = linkDownloadResult.Link.Id,
                            Url = linkDownloadResult.Link.Url
                        });
                    }

                    SaveDocument(url, linkId, linkDownloadResult.Content);
                }
            });
        }

        private void DownloadDocuments(Link[] links)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears all stored data collected by the spider
        /// </summary>
        public void ClearRepositories()
        {
            this._documentsRepository.RemoveAllDocuments();
        }

        private Document GetDocumentContentFromFile(string docFileName)
        {
            Document result = new Document();

            using (FileStream fs = new FileStream(docFileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    result.Url = reader.ReadLine();

                    string linkIdString = reader.ReadLine();
                    Guid linkId;
                    if (Guid.TryParse(linkIdString, out linkId))
                    {
                        result.LinkId = linkId;
                    }

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

        private void SaveDocument(string url, Guid linkId, string document, string subfolderName = null, string fileName = null)
        {
            string filePath = !string.IsNullOrEmpty(subfolderName) ? 
                string.Format("{0}\\{1}", Properties.Settings.Default.DocumentsFolderPath, subfolderName) : 
                Properties.Settings.Default.DocumentsFolderPath;

            string documentFilePath = string.Format("{0}\\{1}.txt", filePath,
                !string.IsNullOrEmpty(fileName) ? fileName : RemoveInvalidCharacters(url));

            StringBuilder fileContentBuilder = new StringBuilder();
            fileContentBuilder.AppendLine(url);
            fileContentBuilder.AppendLine(linkId.ToString());
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
