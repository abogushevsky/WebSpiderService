using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace WebSpiderService.Common.Entities
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Link Parent { get; set; }

        [MaxLength(512)]
        [Required]
        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastDownloadedDate { get; set; }

        [Required]
        public Guid LinkContentTypeId { get; set; }

        public virtual LinkContentType LinkContentType { get; set; }
    }
}