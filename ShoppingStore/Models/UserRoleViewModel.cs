using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models
{
    public class UserRoleViewModel
    {

  
        public int UserId { get; set; }
     
        public int RoleId { get; set; }
   
        public virtual UserViewModel User { get; set; }
    
        public virtual RoleViewModel Role { get; set; }
    }
}