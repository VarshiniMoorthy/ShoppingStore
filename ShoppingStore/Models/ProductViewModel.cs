using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models
{
    public class ProductViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Slug { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public string CatagoryName { get; set; }
        [Required]

        public int CatagoryId { get; set; }
        [Required]

        public string ImageName { get; set; }

        [Required]

        public virtual CategoryViewModel Category { get; set; }
    }
}