using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class ProductVariant
    {
        [Key]
        public int productVariantId { get; set; }

        public DateTime variantCreationDate { get; set; }

        [Required]
        public int prodctId { get; set; }
        [ForeignKey("productId")]
        public Product product { get; set; }

        public List<VariantAttribute> variantAttributes { get; set; }

        public List<VariantImage> variantImages { get; set; } 

    }
}
