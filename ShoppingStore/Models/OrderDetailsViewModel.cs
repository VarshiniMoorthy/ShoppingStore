using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models
{
    public class OrderDetailsViewModel
    {
       
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
     

        public virtual OrderViewModel Orders { get; set; }
      
        public virtual UserViewModel Users { get; set; }
       
        public virtual ProductViewModel Product { get; set; }
    }
}