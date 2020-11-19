using AutoMapper;
using ShoppingStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingStore.Entity;
using ShoppingStore.BL;
using System.IO;

namespace ShoppingStore.Controllers
{
    public class ShopController : Controller
    {
        IShopStoreBL shopStoreBL;
        public ShopController()
        {
            shopStoreBL = new ShopStoreBL();
        }
        // GET: Shop
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Pages");
        }
        public ActionResult CategoryMenuPartial()
        {

            List<CategoryViewModel> categoryVMList;

            List<Category> categoryList;
            // Get all pages except home

            

            categoryList = shopStoreBL.ListOfCategory();


            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); });
            IMapper mapper = config.CreateMapper();
            categoryVMList = mapper.Map<List<Category>, List<CategoryViewModel>>(categoryList);
            // Init the list

            // Return partial with list
            return PartialView(categoryList);
        }
        // GET: /shop/category/name
        public ActionResult Category(string name)
        {
            // Declare a list of ProductVM
           // List<ProductVm> productVMList;
            List<Product> productList;
            ProductViewModel fetchedData = null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductViewModel, Product>(); });
            IMapper mapper = config.CreateMapper();
            Product sensitiveData = mapper.Map<ProductViewModel, Product>(fetchedData);
           
                
                // Get category id
                int categoryId = shopStoreBL.GetCategoryId(name);

            // Init the list
                productList = shopStoreBL.ListOfProducts(categoryId);

            // Get category name
            string categoryName = shopStoreBL.GetCategoryName(categoryId);
            ViewBag.CategoryName = categoryName;
            
            // Return view with list
            return View(productList);
        }
        //[ActionName("product-details")]
        public ActionResult ProductDetails(string name)
        {
            // Declare the VM and DTO
            //ProductViewModel model;
            Product product;

            // Init product i
            int id = 0;

         
                // Check if product exists
                if (shopStoreBL.ProductDetails(name))
                {
                    return RedirectToAction("Index", "Shop");
                }

            // Init productDTO
            product = shopStoreBL.GetProduct(name);

                // Get id
                id = product.Id;
        

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
            IMapper mapper = config.CreateMapper();
            ProductViewModel sensitiveData = mapper.Map<Product, ProductViewModel>(product);
            // Init model
           
            

            // Get gallery images
            product.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs"))
                                                .Select(fn => Path.GetFileName(fn));

            // Return view with model
            return View("ProductDetails", sensitiveData);
        }
    }
}