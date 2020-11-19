using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Entity
{
   public class Login
    { 
   
        public string Username { get; set; }
        
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
