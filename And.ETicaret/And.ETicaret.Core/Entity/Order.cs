using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.ETicaret.Core.Entity
{
   public class Order:EntityBase
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserAddressId { get; set; }
        [ForeignKey("UserAddressId")]
        public UserAddress UserAddress { get; set; }
        public decimal TotalProductPrice { get; set; }
        public decimal TotalTaxPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPrice { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public virtual List<OrderPayment> OrderPayments { get; set; } //tabloda olmayan kolon virtual
        public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
