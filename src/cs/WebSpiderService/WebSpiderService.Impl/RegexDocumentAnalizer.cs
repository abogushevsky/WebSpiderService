using System;
using System.Collections.Generic;
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

            List<string> result = new List<string>();

            MatchCollection matches = this._hrefRegex.Matches(documentContent);

            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result.ToArray();
        }
    }
}