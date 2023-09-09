using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class Attribute
    {
        [Key]  
        public int attributeId { get; set; }

        [Required]
        public string attributeName { get; set; }

        [Required]
        public string description { get; set; }

        public List<ProductVariant> variants { get; set; }

        public List<VariantAttribute> variantAttributes { get; set; }

    }
}
