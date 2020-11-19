using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingStore.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ShoppingStore.Entity;

namespace ShoppingStore.Models
{
    public class PageViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 10)]
        [AllowHtml]
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSideImage { get; set; }
       

        public PageViewModel()
        {

        }
        public PageViewModel(PageViewModel page)
        {
            Id = page.Id;
            Title = page.Title;
            Description = page.Description;
            Body = page.Body;
            Sorting = page.Sorting;
            HasSideImage = page.HasSideImage;
        }

        public PageViewModel(Page pages)
        {
            Pages = pages;
        }
    }
}
    