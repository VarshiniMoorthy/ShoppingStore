using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;
using ShoppingStore.DAL;

namespace ShoppingStore.BL
{
    public interface IAccountBL
    {
        bool Login(Login login);
        bool CreateAccount(User user);
        User ShowUser(string userName);
        User UserProfile(string userName);
        //User EditProfile(string userName);
        //UserProfileVm UseProfileVm();
        //UserProfileVm UserProfileVm(UserProfile model);
    }
    public class AccountBL :IAccountBL
    {
        IAccountRepository accountRepository;
        public AccountBL()
        {
            accountRepository = new AccountRepository();
        }
        public bool Login(Login login)
        {
            return accountRepository.Login(login);
        }
        public bool CreateAccount(User user)
        {
           return accountRepository.CreateAccount(user);
        }
        public User ShowUser(string userName)
        {
            return accountRepository.ShowUser(userName);
        }
        public User UserProfile(string userName)
        {
            return accountRepository.UserProfile(userName);
        }
        //public EditProfile(string userName)
        //{
        //    return accountRepository.EditProfile(userName);
        //}
    }
}
