using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.ETicaret.Core.Entity
{
   public class Category:EntityBase
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
