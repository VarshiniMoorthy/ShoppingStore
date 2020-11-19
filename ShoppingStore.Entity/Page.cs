using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Entity
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
  
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSideImage { get; set; }

        public Page(Page page)
        {
            Id = page.Id;
            Title = page.Title;
            Description = page.Description;
            Body = page.Body;
            Sorting = page.Sorting;
            HasSideImage = page.HasSideImage;
        }

    }
}
