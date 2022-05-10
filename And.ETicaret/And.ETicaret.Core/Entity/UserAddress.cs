using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.ETicaret.Core.Entity
{
   public class UserAddress:EntityBase
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}
