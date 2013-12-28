using System.Net.Mime;

namespace WebSpiderService.Common.Entities
{
    public class Link
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Url { get; set; }

        public LinkContentType LinkContentType { get; set; }
    }
}