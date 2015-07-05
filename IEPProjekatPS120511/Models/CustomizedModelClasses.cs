using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEPProjekatPS120511.Models
{
    [MetadataType(typeof(SW_ProductMetaData))]
    public partial class SW_Product
    {
        [NotMapped]
        public HttpPostedFileBase ImageToUpload { get; set; }
        public HttpPostedFileBase LogoToUpload { get; set; }
    }

    public class SW_ProductMetaData
    {
        [Required, Display(Name = "Product Name")]
        public string Name { get; set; }

        // [MaxLength(100, ErrorMessage="Image must contain less than 100 bytes.")]
        public byte[] Image { get; set; }
        public byte[] Logo { get; set; }
    }
}