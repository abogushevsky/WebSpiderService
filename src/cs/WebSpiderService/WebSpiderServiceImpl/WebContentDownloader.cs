﻿using System;
using System.Net;
using System.Threading.Tasks;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderServiceImpl
{
    public class WebContentDownloader : IContentDownloader
    {
        public async Task<string> DownloadUrlAsync(string url)
        {
            WebClient webClient = new WebClient();
            return await webClient.DownloadStringTaskAsync(new Uri(url));
        }

        public string DownloadUrl(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
                return webClient.DownloadString(uri);
            }
            catch
            {
                return null;
            }
        }
    }
}