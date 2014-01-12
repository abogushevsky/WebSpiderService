using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSpiderService.Common.Entities
{
    //[Table("LinkContentTypes")]
    public class LinkContentType
    {
        [Key]
        public Guid Id { get; set; }

      //  [Column("ContentType")]
        [MaxLength(128)]
        [Required]
        public string ContentType { get; set; }

     //   [Column("FileExtension")]
        [MaxLength(16)]
        public string FileExtension { get; set; }
    }
}