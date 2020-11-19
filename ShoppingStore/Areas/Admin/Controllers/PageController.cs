using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShoppingStore.BL;
using ShoppingStore.Entity;
using ShoppingStore.Models;

namespace ShoppingStore.Areas.Admin.Controllers
{
    public class PageController : Controller
    {
        // GET: Admin/Page
        IPageBL pageBL;

        public PageController()
        {
            pageBL = new PageBL();
        }
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult ListOfPages()
        {
            List<Page> page = pageBL.ListOfPage();
            return View(page);
        }
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPage(PageViewModel data)
        {
            Page sensitiveData = null;
            if (ModelState.IsValid)
            {
             
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<PageViewModel, Page>(); });
                IMapper mapper = config.CreateMapper();
                 sensitiveData = mapper.Map<PageViewModel, Page>(data);
                pageBL.AddPage(sensitiveData);
            }
            
            string description;
          
            if (string.IsNullOrWhiteSpace(data.Description))
            {
                description = data.Title.Replace(" ", "-").ToLower();
            }
            else
            {
                description = data.Description.Replace(" ", "-").ToLower();
            }
    
            //Check for already exists one
            if (pageBL.CheckPage(sensitiveData))
            {
                ModelState.AddModelError("", "The Title or description already exists");
            }
           sensitiveData.Title = data.Title;
            sensitiveData.Description = description;
            sensitiveData.HasSideImage = data.HasSideImage;
            sensitiveData.Sorting = 100;
            pageBL.AddPage(sensitiveData);
            //adding and saving to database
           
            //Set tempdata message
            TempData["Success-Message"] = "You have added a new page! ";
                //Redirect action
                return RedirectToAction("AddPage");
        }

        
        [HttpGet]
        public ActionResult EditPage(int id)
        {
        
        Page fetchedData = pageBL.EditPage(id);



            if (fetchedData == null)
            {
                return Content("The page does not exists!!");

            }
            else
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Page, PageViewModel>(); });
                IMapper mapper = config.CreateMapper();
                PageViewModel sensitiveData = mapper.Map<Page, PageViewModel>(fetchedData);

                return View(sensitiveData);
            }
        }

        [HttpPost]
        public ActionResult EditPage(PageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

                string Description = "homePage";
                int id = model.Id;

                Page page = pageBL.EditPage(id);
                page.Title = model.Title;
                if (model.Description != "homePage")
                {
                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        Description = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        Description = model.Description.Replace(" ", "-").ToLower();
                    }
                }

                if (pageBL.CheckTitle(id,page))
                {
                    ModelState.AddModelError("", "The Title or Description is already exists.");
                    return View(model);
                }
                page.Description = model.Description;
                page.Body = model.Body;
                page.HasSideImage = model.HasSideImage;

            
            TempData["Success-message"] = "You have edited the records !";
            return RedirectToAction("EditPage");
        }
        [HttpGet]
        public ActionResult PageDetails(int id)
        {
            
            Page page;
         
                page = pageBL.EditPage(id);
                if (page == null)
                {
                    return Content("The page does not exists");
                }
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Page, PageViewModel>(); });
            IMapper mapper = config.CreateMapper();
            PageViewModel sensitiveData = mapper.Map<Page, PageViewModel>(page);
           // pageVm = new PageVm(pageDTO);
            
            return View(sensitiveData);
        }

        [HttpGet]
        public ActionResult DeletePage(int id)
        {

                Page page = pageBL.EditPage(id);
                 pageBL.DeletePage(id, page);

            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ReorderPages(int[] id)
        {
           
            pageBL.ReorderPages(id);

        }
        public ActionResult EditViewBar()
        {


                SideBar sideBar = pageBL.EditViewBar();
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SideBar, SideBarViewModel>(); });
                IMapper mapper = config.CreateMapper();
                SideBarViewModel sensitiveData = mapper.Map<SideBar, SideBarViewModel>(sideBar);
               
            
            return View(sensitiveData);
        }
        [HttpPost]
        public ActionResult EditViewBar(SideBarViewModel sideBar)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SideBarViewModel, SideBar>(); });
            IMapper mapper = config.CreateMapper();
            SideBar sensitiveData = mapper.Map<SideBarViewModel, SideBar>(sideBar);

            pageBL.EditViewBar(sensitiveData);
            
            TempData["Success-Message"] = "You have edited a side bar";
            return RedirectToAction("EditViewBar");
        }

    }

}