using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ShoppingStore.BL;
using ShoppingStore.Entity;
using ShoppingStore.Models;


namespace ShoppingStore.Controllers
{
    public class PagesController : Controller
    {
        IPagesBL pagesBL;
        public PagesController()
        {
            pagesBL = new PagesBL();
        }
        // GET: Pages
        public ActionResult CheckPage(string page = "")
        {
            // Get/set page slug
            if (page == "")
                page = "homePages";

            // Declare model and DTO
            PageViewModel model;
            Page pages;

            // Check if page exists
           if (!pagesBL.CheckPage(page))
                {
                    return RedirectToAction("Index", new { page = "" });
                }


            // Get page DTO

            pages = pagesBL.GetPage(page);
         

            // Set page title
            ViewBag.PageTitle = pages.Title;

            // Check for sidebar
            if (pages.HasSideImage == true)
            {
                ViewBag.Sidebar = "Yes";
            }
            else
            {
                ViewBag.Sidebar = "No";
            }

            // Init model
            model = new PageViewModel(pages);

            // Return view with model
            return View(model);
        }
        public ActionResult PagesMenuPartial()
        {
            // Declare a list of PageVM
            List<PageViewModel> pageVMList;

            List<Page> pageList;
            // Get all pages except home

            pageList = pagesBL.GetAllPage();
            
            var config = new MapperConfiguration(cfg => { cfg.CreateMap <Page, PageViewModel>(); });
            IMapper mapper = config.CreateMapper();
            pageVMList = mapper.Map<List<Page>, List<PageViewModel>>(pageList);

            // Return partial view with list
            return PartialView(pageVMList);
        }
        public ActionResult SidebarPartial()
        {
            // Declare model
            SideBarViewModel model;

            // Init model

               SideBar sideBar = pagesBL.SideBar();

                model = new SideBarViewModel(sideBar);
           

            // Return partial view with model
            return PartialView(model);
        }
    }
}
    
