using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using ShoppingStore.Entity;

namespace ShoppingStore.DAL
{
    public interface IAccountRepository
    {
        bool Login(Login login);
        bool CreateAccount(User user);
        User ShowUser(string userName);
        User UserProfile(string userName);

        //User EditProfile(string userName);
    }
    public class AccountRepository : IAccountRepository
    {
        public bool Login(Login login)
        {
            bool isValid = false;

            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {

                if (shoppingStoreContext.Users.Any(x => x.UserName.Equals(login.Username) && x.Password.Equals(login.Password)))
                {
                    isValid = true;
                }
            }
            return isValid;

        }
        public bool CreateAccount(User user)


        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                // Make sure username is unique
                bool status = false;
                // Make sure username is unique
                if (shoppingStoreContext.Users.Any(x => x.UserName.Equals(user.UserName)))
                {
                    status = true;
                    user.UserName = "";
                    //  return View("CreateAccount", model);
                }

                // Add the DTO
                shoppingStoreContext.Users.Add(user);

                // Save
                shoppingStoreContext.SaveChanges();

                // Add to UserRolesDTO
                int id = user.Id;

                UserRole userRoles = new UserRole()
                {
                    UserId = id,
                    RoleId = 2
                };

                shoppingStoreContext.UserRoles.Add(userRoles);
                shoppingStoreContext.SaveChanges();
                return status;

            }
        }
        public User ShowUser(string userName)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                // Get the user
                User userValue = shoppingStoreContext.Users.FirstOrDefault(x => x.UserName == userName);

                return userValue;
            }
        }
        public User UserProfile(string userName)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Users.FirstOrDefault(x => x.UserName == userName);
            }
        }
        //public User EditProfile(string userName)
        //{
        //    using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
        //    {
        //        if (shoppingStoreContext.Users.Where(x => x.Id != model.Id).Any(x => x.UserName == username))
        //        {
        //        }

    }
}

