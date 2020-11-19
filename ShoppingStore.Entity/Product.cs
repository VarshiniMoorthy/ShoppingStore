using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ShoppingStore.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
     
        public string Name { get; set; }
        public string Slug { get; set; }
     
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CatagoryName { get; set; }
     
        public int CatagoryId { get; set; }
        public string ImageName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
        public Product()
        {
        }

        public Product(Product row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Description = row.Description;
            Price = row.Price;
            CatagoryName = row.CatagoryName;
            CatagoryId = row.CatagoryId;
            ImageName = row.ImageName;
        }

    }
}
