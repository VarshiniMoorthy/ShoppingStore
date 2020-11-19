using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Entity
{
    public class SideBar
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
    }
}
