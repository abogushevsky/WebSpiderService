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

      //  [Column("ParentId")]
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
       // [InverseProperty("Links")]
        public virtual Link Parent { get; set; }

       // [Column("Url")]
        [MaxLength(512)]
        [Required]
        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }

      //  [Column("LinkContentTypeId")]
     //   [Required]
        public Guid LinkContentTypeId { get; set; }

     //   [ForeignKey("LinkContentTypeId")]
    //    [InverseProperty("LinkContentTypes")]
        public virtual LinkContentType LinkContentType { get; set; }
    }
}