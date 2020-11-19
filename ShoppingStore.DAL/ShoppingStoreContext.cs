using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;

namespace ShoppingStore.DAL
{
    public class ShoppingStoreContext:DbContext
    {
       
            public DbSet<Page> Page { set; get; }
            public DbSet<SideBar> Bars { get; set; }
            public DbSet<Category> Catagories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<UserRole> UserRoles { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetails> OrderDetails { get; set; }


        }
    }
}
