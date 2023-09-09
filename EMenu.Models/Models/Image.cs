using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class Image
    {
        [Key]
        public int imageId { get; set; }

        [Required]
        public string imageURL { get; set; }

        public List<Product> products { get; set; }

        public List<VariantImage> variantImages { get; set; }

    }
}
