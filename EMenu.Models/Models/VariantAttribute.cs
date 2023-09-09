using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class VariantAttribute
    {
        [Required]
        [Key]
        public int productVariantId { get; set; }
        [ForeignKey("productVariantId")]
        public ProductVariant productVariant;

        [Required]
        [Key]
        public int attributeId { get; set; }
        [ForeignKey("attributeId")]
        public Attribute attribute;

        public string attributeDescription { get; set; }

    }
}
