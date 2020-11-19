using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Entity
{
   public class UserProfile
    {
        [Key]
        public int Id { get; set; }
       
        public string UserName { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string EmailId { get; set; }
        
        public string Address { get; set; }
        
        public string ContactNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
