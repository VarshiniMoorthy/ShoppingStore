﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Entity
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("OrderId")]

        public virtual Order Orders { get; set; }
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
