using ShoppingStore.DAL;
using ShoppingStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.BL
{
    public interface IPagesBL
    {
        bool CheckPage(string page);
        Page GetPage(string page);
        //PageViewModel GetAllPage();
        List<Page> GetAllPage();
        SideBar SideBar();

    }
    public class PagesBL:IPagesBL
    {
        IPagesRepository pagesRepository; 
        public PagesBL()
        {
            pagesRepository = new PagesRepository();
        }
        public bool CheckPage(string page)
        {
            return pagesRepository.CheckPage(page);
        }
        public Page GetPage(string page)
        {
            return pagesRepository.GetPage(page);
        }

        public SideBar SideBar()
        {
            return pagesRepository.SideBar();
        }

       public List<Page> GetAllPage()
        {
            return pagesRepository.GetAllPage();
        }
        
    }
}
