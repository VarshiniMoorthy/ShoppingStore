using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sorting { get; set; }
        public Category()
        {

        }
        public Category(Category row)
        {
            Id = row.Id;
            Name = row.Name;
            Description = row.Description;
            Sorting = row.Sorting;
        }
    }
}
