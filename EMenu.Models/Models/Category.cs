using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Models.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string categoryName { get; set; }

        [MaxLength(250)]
        public string? categoryDescription { get; set; }

        List<Product> products { get; set; }
    }
}
