﻿using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using WebSpiderService.Common.Interfaces;

namespace WebSpiderService.Impl
{
    /// <summary>
    /// Implementation of IDocumentAnalizer that uses regexes to analize document
    /// </summary>
    public class RegexDocumentAnalizer : IDocumentAnalizer
    {
        private readonly Regex _hrefRegex = new Regex("href\\s*=\\s*[\"']?([^\"' >]+)[\"' >]", RegexOptions.Compiled);

        /// <summary>
        /// Method finds and returns urls in document content
        /// </summary>
        /// <param name="documentContent"></param>
        /// <returns></returns>
        public string[] GetLinksFromDocument(string documentContent)
        {
            Contract.Requires(documentContent != null);

            Match matchResult = this._hrefRegex.Match(documentContent);

            if (matchResult.Success)
            {
                
            }

            throw new NotImplementedException();
        }
    }
}