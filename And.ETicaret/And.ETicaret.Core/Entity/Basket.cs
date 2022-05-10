using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.ETicaret.Core.Entity
{
  public  class Basket:EntityBase
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }
    }
}
