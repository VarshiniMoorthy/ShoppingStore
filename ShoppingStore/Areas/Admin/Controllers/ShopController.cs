using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingStore.Models;
using ShoppingStore.Entity;
using AutoMapper;
using ShoppingStore.BL;
using System.Web.Helpers;
using PagedList;
using System.IO;

namespace ShoppingStore.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        IShopBL shopBL;
        public ShopController()
        {
            shopBL = new ShopBL();
        }
        // GET: Admin/Cart
        public ActionResult Category()
        {
            // List<CategoryViewModel> catagoryList;
            List<Category> categories;
            CategoryViewModel fetchedData = null;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); });
            IMapper mapper = config.CreateMapper();
            Category sensitiveData = mapper.Map<CategoryViewModel, Category>(fetchedData);
            categories = shopBL.Category();

            return View(categories);
        }
        [HttpPost]
        public string AddNewCategory(string categoryname)
        {
            string id;
            Category category = new Category();
            category.Name = categoryname;
            category.Description = categoryname.Replace(" ", "-").ToLower();
            category.Sorting = 100;

            if (shopBL.AddNewCategory(categoryname, category))
            {
                return "titletaken";
            }
            id = category.Id.ToString();
            return id;
        }


        [HttpPost]
        public void ReorderCategories(int[] id)
        {
            shopBL.ReorderCategories(id);
        }
        //Deleteing the catagory
        [HttpGet]
        public ActionResult DeleteCatagory(int id)
        {
            shopBL.DeleteCatagory(id);
            return RedirectToAction("Catagory");
        }

        [HttpPost]
        public string RenameCategory(string newCategoryName, int id)
        {

            // Check category name is unique
            if (shopBL.RenameCategory(newCategoryName, id))
            {
                return "titletaken";
            }

            // Get DTO
            return "ok";
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            // Init model
            ProductViewModel fetchedData = null;


            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductViewModel, Product>(); });
            IMapper mapper = config.CreateMapper();
            Product sensitiveData = mapper.Map<ProductViewModel, Product>(fetchedData);
            shopBL.AddProduct(sensitiveData);
            Product fetchedDatas = null;

            var configs = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
            IMapper mappers = config.CreateMapper();
            ProductViewModel sensitiveDatas = mapper.Map<Product, ProductViewModel>(fetchedDatas);

            // Add select list of categories to model
            // Return view with model
            return View(sensitiveDatas);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model, HttpPostedFileBase file)
        {
            Product sensitiveData = null;
            // Check model state
            if (!ModelState.IsValid)
            {
                // Init model
                ProductViewModel fetchedData = null;


                var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductViewModel, Product>(); });
                IMapper mapper = config.CreateMapper();
                sensitiveData = mapper.Map<ProductViewModel, Product>(fetchedData);
                shopBL.AddProduct(sensitiveData);
                Product fetchedDatas = null;

                var configs = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); });
                IMapper mappers = config.CreateMapper();
                ProductViewModel sensitiveDatas = mapper.Map<Product, ProductViewModel>(fetchedDatas);
                return View(model);
            }
            if (!shopBL.AddProduct(sensitiveData))
            {
                // Make sure product name is unique
                ModelState.AddModelError("", "That product name is taken!");
                return View(model);
            }

            // Declare product id
            int id;


            Product product = new Product();

            product.Name = model.Name;
            product.Slug = model.Name.Replace(" ", "-").ToLower();
            product.Description = model.Description;
            product.Price = model.Price;
            product.CatagoryId = model.CatagoryId;

            Category category = shopBL.SaveProduct(product);
            product.CatagoryName = category.Name;


            // Get the id
            id = product.Id;


            // Set TempData message
            TempData["SM"] = "You have added a product!";

            #region Upload Image

            // Create necessary directories
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thumbs");

            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);

            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);

            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);

            if (!Directory.Exists(pathString4))
                Directory.CreateDirectory(pathString4);

            if (!Directory.Exists(pathString5))
                Directory.CreateDirectory(pathString5);

            // Check if a file was uploaded
            if (file != null && file.ContentLength > 0)
            {
                // Get file extension
                string ext = file.ContentType.ToLower();

                // Verify extension
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {

                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View(model);
                }
            }

            // Init image name
            string imageName = file.FileName;


            Product productImage = shopBL.UploadImage(id);
            productImage.ImageName = imageName;

            // Set original and thumb image paths
            var path = string.Format("{0}\\{1}", pathString2, imageName);
            var path2 = string.Format("{0}\\{1}", pathString3, imageName);

            // Save original
            file.SaveAs(path);

            // Create and save thumb
            WebImage img = new WebImage(file.InputStream);
            img.Resize(300, 250);
            img.Save(path2);


            #endregion

            // Redirect
            return RedirectToAction("AddProduct");
        }
        public ActionResult Products(int? page, int? categoryId)
        {
            // Declare a list of ProductVM
            // List<ProductViewModel> listOfProductVM;
            List<Product> listOfProduct;
            ProductViewModel fetchedData = null;


            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductViewModel, Product>(); });
            IMapper mapper = config.CreateMapper();
            Product sensitiveData = mapper.Map<ProductViewModel, Product>(fetchedData);


            // Set page number
            var pageNumber = page ?? 1;


            // Init the list
            listOfProduct = shopBL.ListOfProduct(sensitiveData, categoryId);

            // Populate categories select list
            ViewBag.Categories = (shopBL.SelectListItem(), "Id", "Name");

            // Set selected category
            ViewBag.SelectedCategory = categoryId.ToString();

            // Set pagination
            var onePageOfProducts = listOfProduct.ToPagedList(pageNumber, 3);
            ViewBag.OnePageOfProducts = onePageOfProducts;

            // Return view with list
            return View(listOfProduct);
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            // Declare productVM
            Product model;


            // Get the product
            Product product = shopBL.EditProduct(id);

            // Make sure product exists
            if (product == null)
            {
                return Content("That product does not exist.");
            }

            // init model
            model = new Product(product);

            // Make a select list
            // model.Categories = new SelectList(db.Catagories.ToList(), "Id", "Name");

            // Get all gallery images
            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs"))
                                            .Select(fn => Path.GetFileName(fn));


            // Return view with model
            return View(model);
        }
        [HttpPost]

        public ActionResult EditProduct(ProductViewModel model, HttpPostedFileBase file)
        {
            ProductViewModel fetchedData = null;


            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductViewModel, Product>(); });
            IMapper mapper = config.CreateMapper();
            Product sensitiveData = mapper.Map<ProductViewModel, Product>(fetchedData);

            // Get product id
            int id = sensitiveData.Id;

            // Populate categories select list and gallery images

            sensitiveData.Categories = new SelectList(shopBL.SelectListItem(), "Id", "Name");

            sensitiveData.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs"))
                                                .Select(fn => Path.GetFileName(fn));

            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Make sure product name is unique

            if (shopBL.CheckProduct(sensitiveData, id))
            {
                ModelState.AddModelError("", "That product name is taken!");
                return View(model);
            }


            // Update product

            Product product = shopBL.UpdateProduct(id,sensitiveData);
            product.Name = model.Name;
            product.Slug = model.Name.Replace(" ", "-").ToLower();
            product.Description = model.Description;
            product.Price = model.Price;
            product.CatagoryId = model.CatagoryId;
            product.ImageName = model.ImageName;
    
            // Set TempData message
            TempData["SM"] = "You have edited the product!";

            #region Image Upload

            // Check for file upload
            if (file != null && file.ContentLength > 0)
            {

                // Get extension
                string ext = file.ContentType.ToLower();

                // Verify extension
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {
                  
                        ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                        return View(model);
                    
                }

                // Set uplpad directory paths
                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                var pathString1 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");

                // Delete files from directories

                DirectoryInfo di1 = new DirectoryInfo(pathString1);
                DirectoryInfo di2 = new DirectoryInfo(pathString2);

                foreach (FileInfo file2 in di1.GetFiles())
                    file2.Delete();

                foreach (FileInfo file3 in di2.GetFiles())
                    file3.Delete();

                // Save image name

                string imageName = file.FileName;


                Product product1 = shopBL.FindProduct(id);
                    product1.ImageName = imageName;

              
                

                // Save original and thumb images

                var path = string.Format("{0}\\{1}", pathString1, imageName);
                var path2 = string.Format("{0}\\{1}", pathString2, imageName);

                file.SaveAs(path);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(192, 250);
                img.Save(path2);
            }

            #endregion

            // Redirect
            return RedirectToAction("EditProduct");
        }
        // GET: Admin/Shop/DeleteProduct/id
        public ActionResult DeleteProduct(int id)
        {
            // Delete product from DB

            shopBL.DeleteProduct(id);
           

            // Delete product folder
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));
            string pathString = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());

            if (Directory.Exists(pathString))
                Directory.Delete(pathString, true);

            // Redirect
            return RedirectToAction("Products");
        }

        // POST: Admin/Shop/SaveGalleryImages
        [HttpPost]
        public void SaveGalleryImages(int id)
        {
            // Loop through files
            foreach (string fileName in Request.Files)
            {
                // Init the file
                HttpPostedFileBase file = Request.Files[fileName];

                // Check it's not null
                if (file != null && file.ContentLength > 0)
                {
                    // Set directory paths
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                    string pathString1 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery");
                    string pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thumbs");

                    // Set image paths
                    var path = string.Format("{0}\\{1}", pathString1, file.FileName);
                    var path2 = string.Format("{0}\\{1}", pathString2, file.FileName);

                    // Save original and thumb

                    file.SaveAs(path);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(192, 250);
                    img.Save(path2);
                }

            }
        }
        // POST: Admin/Shop/DeleteImage
        [HttpPost]
        public void DeleteImage(int id, string imageName)
        {
            string fullPath1 = Request.MapPath("~/Images/Uploads/Products/" + id.ToString() + "/Gallery/" + imageName);
            string fullPath2 = Request.MapPath("~/Images/Uploads/Products/" + id.ToString() + "/Gallery/Thumbs/" + imageName);

            if (System.IO.File.Exists(fullPath1))
                System.IO.File.Delete(fullPath1);

            if (System.IO.File.Exists(fullPath2))
                System.IO.File.Delete(fullPath2);
        }

    }
}

    