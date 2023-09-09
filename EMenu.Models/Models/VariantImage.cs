using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class VariantImage
    {
        [Required]
        [Key]
        public int productVariantId;
        [ForeignKey("productVariantId")]
        public ProductVariant productVariant;

        [Required]
        [Key]
        public int imageId;
        [ForeignKey("imageId")]
        public Image image;
    }
}
