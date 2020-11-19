using ShoppingStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.DAL;

namespace ShoppingStore.BL
{

    public interface IPageBL
    {
        List<Page> ListOfPage();
       void AddPage(Page data);
        Page EditPage(int id);
        bool CheckTitle(int id,Page page);
        void DeletePage(int id,Page page);
        void ReorderPages(int[] id);
        SideBar EditViewBar();
        void EditViewBar(SideBar sideBar);
        bool CheckPage(Page page);

    }
    public class PageBL:IPageBL
    {
        IPageRepository pageRepository;
        public PageBL()
        {
            pageRepository = new PageRepository();
        }
        public List<Page> ListOfPage()
        {
         return  pageRepository.ListOfPage();
        }
        public void AddPage(Page data)
        {
            pageRepository.AddPage(data);
        }
        public Page EditPage(int id)
        {
            return pageRepository.EditPage(id);
        }

        public bool CheckTitle(int id,Page page)
        {
            return pageRepository.CheckTitle(id,page);
        }
        public void DeletePage(int id,Page page)
        {
            pageRepository.DeletePage(id,page);
        }
        public void ReorderPages(int[] id)
        {
            pageRepository.ReorderPages(id);
        }
        public SideBar EditViewBar()
        {
           return pageRepository.EditViewBar();
        }
        public void EditViewBar(SideBar sideBar)
        {
            pageRepository.EditViewBar(sideBar);
        }
        public bool CheckPage(Page page)
        {
          return  pageRepository.CheckPage(page);
        }

    }
}
