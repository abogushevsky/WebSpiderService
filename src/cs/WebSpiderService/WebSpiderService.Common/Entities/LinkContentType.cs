using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSpiderService.Common.Entities
{
    public class LinkContentType
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(128)]
        [Required]
        public string ContentType { get; set; }

        [MaxLength(16)]
        public string FileExtension { get; set; }

        [DefaultValue(true)]
        [Required]
        public bool ShouldDownload { get; set; }
    }
}