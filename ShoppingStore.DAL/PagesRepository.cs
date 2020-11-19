using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;

namespace ShoppingStore.DAL
{
    public interface IPagesRepository
    {
        bool CheckPage(string page);
         Page GetPage(string page);
        List<Page> GetAllPage();
        SideBar SideBar();
    }
    public class PagesRepository : IPagesRepository
    {
        public bool CheckPage(string page)
        {
            bool status = false;
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                if (shoppingStoreContext.Page.Any(x => x.Description.Equals(page)))
                {
                    status = true;
                }
                return status;
            }
        }
        public Page GetPage(string page)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Page.Where(x => x.Description == page).FirstOrDefault();
            }
        }
        public SideBar SideBar()
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Bars.Find(1);
            }
        }
        public List<Page> GetAllPage()
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Page.ToArray().OrderBy(x => x.Sorting).Where(x => x.Title != "homePages").ToList();
            }
        }
    }
}
