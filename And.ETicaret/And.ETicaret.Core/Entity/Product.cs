using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.ETicaret.Core.Entity
{
   public class Product:EntityBase
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public decimal Discount { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
