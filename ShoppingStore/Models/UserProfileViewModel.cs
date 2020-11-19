using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingStore.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a - z0 - 9_ -]{3, 15}$", ErrorMessage = "Should contain a-z & underscore and numbers and size between 3 to 15")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"[a-z]{1,10}", ErrorMessage = "Not in correct format")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"[a-z]{1,10}", ErrorMessage = "Not in correct format")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\ -]\s*)?|[0]?)?[789]\d{9}|(\d[ -]?){10}\d$", ErrorMessage = "Not in correct format")]
        public string ContactNumber { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = " Password must be at least 4 characters, no more than 8 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit.")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public UserProfileViewModel()
        {

        }
        public UserProfileViewModel(User row)
        {
            Id = row.Id;
            UserName = row.UserName;
            LastName = row.LastName;
            EmailId = row.EmailId;
            Address = row.Address;
            ContactNumber = row.ContactNumber;
            Password = row.Password;


        }
    }
}