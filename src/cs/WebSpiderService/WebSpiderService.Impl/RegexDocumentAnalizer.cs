using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using WebSpiderService.Common.Entities;
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
        /// <returns></returns>
        public string[] GetLinksFromDocument(Document document)
        {
            Contract.Requires(document != null);

            List<string> result = new List<string>();

            MatchCollection matches = this._hrefRegex.Matches(document.Content);
            foreach (Match match in matches)
            {
                result.Add(RemoveHref(match.Value));
            }

            return result.ToArray();
        }

        private string RemoveHref(string source)
        {
            if (source.Contains("href"))
            {
                return source.Replace("href=\"", "").Replace("\"", "");
            }

            return source;
        }
    }
}