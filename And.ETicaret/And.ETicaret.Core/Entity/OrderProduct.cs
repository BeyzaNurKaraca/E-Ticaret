using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.ETicaret.Core.Entity
{
    public class OrderProduct:EntityBase
    {
        public int OrderId { get; set; }
        //[ForeignKey("OrderId")]
        //public Order Order { get; set; }
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
       // [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
