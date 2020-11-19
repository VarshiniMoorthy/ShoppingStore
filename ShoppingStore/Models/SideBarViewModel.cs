using ShoppingStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models
{
    public class SideBarViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public SideBarViewModel()
        {

        }
        public SideBarViewModel(SideBar row)
        {
            Id = row.Id;
            Body = row.Body;
        }
    }
}
