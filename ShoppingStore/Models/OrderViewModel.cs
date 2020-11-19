using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models
{
    public class OrderViewModel
    {
      
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public virtual UserViewModel User { get; set; }
    }
}