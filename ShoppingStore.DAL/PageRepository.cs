using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;



namespace ShoppingStore.DAL
{
    public interface IPageRepository
    {
        List<Page> ListOfPage();
     
        //Page EditPage(int id);
        bool CheckPage(Page page);
        Page GetPage(string page);
        //Page GetAllPage();
        SideBar SideBar();

        Page EditPage(int id);

       
        bool CheckTitle(int id,Page page);
         void DeletePage(int id, Page page);
        void ReorderPages(int[] id);
        SideBar EditViewBar();
        void EditViewBar(SideBar sideBar);
        void AddPage(Page data);

    }
    public class PageRepository : IPageRepository
    {
        public List<Page> ListOfPage()
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {

                List<Page> page = Context.Page.ToArray().OrderBy(sort => sort.Sorting).Select(sorted => new Page(sorted)).ToList();
                return page;

            }
        }

        public bool CheckPage(Page page)
        {
            bool status = false;
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                if (Context.Page.Any(exists => exists.Title == page.Title) || (Context.Page.Any(exists => exists.Description == page.Description)))
                {
                    status = true;
                }
                return status;
            }
        }
        public Page GetPage(string page)
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                return Context.Page.Where(x => x.Description == page).FirstOrDefault();
            }

        }

        public SideBar SideBar()
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                return Context.Bars.Find(1);
            }
        }
        public Page EditPage(int id)
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                Page page = Context.Page.Find(id);
                return page;
            }

        }
        public bool CheckTitle(int id, Page page)
        {
            bool status = false;
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                if (Context.Page.Where(findId => findId.Id != id).Any(findTitle => findTitle.Title == page.Title) ||
                   Context.Page.Where(findId => findId.Id != id).Any(findTitle => findTitle.Description == page.Description))
                {
                    status = true;
                    Context.SaveChanges();

                }
                return status;
            }
        }
        public void DeletePage(int id, Page page)
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                Context.Page.Remove(page);

                Context.SaveChanges();
            }
        }
        public void ReorderPages(int[] id)
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                int count = 1;
                Page page;

                foreach (var pageId in id)
                {
                    page = Context.Page.Find(pageId);
                    page.Sorting = count;

                    Context.SaveChanges();

                    count++;
                }
            }
        }
        public SideBar EditViewBar()
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                return Context.Bars.Find(1);
            }
        }
        public void EditViewBar(SideBar sideBar)
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                sideBar = Context.Bars.Find(1);
                sideBar.Body = sideBar.Body;
                Context.SaveChanges();
            }
        }
        public void AddPage(Page data)
        {
            using (ShoppingStoreContext Context = new ShoppingStoreContext())
            {
                Context.Page.Add(data);
                Context.SaveChanges();
            }
        }
    }

}
      
