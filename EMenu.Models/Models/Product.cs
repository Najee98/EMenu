using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class Product
    {
        [Key]  
        public int productId { get; set; }

        [Required]  
        public string productName { get; set; }

        [MaxLength(100)]
        public string? productDescription { get; set; }

        public double? productPrice { get; set; }

        [Required]
        public int productImageId { get; set; }
        [ForeignKey("imageId")]
        public Image productImage;

        [Required]
        public int categoryId;
        [ForeignKey("categoryId")]
        public Category productCategory;

        public List<ProductVariant> variants { get; set; }

    }
}
