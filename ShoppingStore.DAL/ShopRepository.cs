using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShoppingStore.Entity;

namespace ShoppingStore.DAL
{
    public interface IShopRepository
    {
        List<Category> Category();
        bool AddNewCategory(string categoryname, Category category);
        void ReorderCategories(int[] id);
        void DeleteCatagory(int id);
        bool RenameCategory(string newCatName, int id);
        bool AddProduct(Product product);
        Category SaveProduct(Product product);
        Product UploadImage(int id);

        List<Product> ListOfProduct(Product product, int ? categoryId);
        List<Category> SelectListItem();

        Product EditProduct(int id);

        bool CheckProduct(Product product, int id);
        Product UpdateProduct(int id, Product product);
        Product FindProduct(int id);

        void DeleteProduct(int id);




    }
    public class ShopRepository : IShopRepository
    {
        public List<Category> Category()
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Catagories.ToArray().OrderBy(x => x.Sorting).Select(x => new Category(x)).ToList();
            }
        }
        public bool AddNewCategory(string categoryname, Category category)
        {
            bool status = false;
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                if (shoppingStoreContext.Catagories.Any(x => x.Name == categoryname))
                {
                    status = true;
                }
                shoppingStoreContext.Catagories.Add(category);
                shoppingStoreContext.SaveChanges();
            }
            return status;


        }
        public void ReorderCategories(int[] id)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                // Set initial count
                int count = 1;

                // Declare PageDTO
                Category category;

                // Set sorting for each page
                foreach (var categoryId in id)
                {
                    category = shoppingStoreContext.Catagories.Find(categoryId);
                    category.Sorting = count;

                    shoppingStoreContext.SaveChanges();

                    count++;

                }
            }
        }

        public void DeleteCatagory(int id)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                Category category = shoppingStoreContext.Catagories.Find(id);
                shoppingStoreContext.Catagories.Remove(category);
                shoppingStoreContext.SaveChanges();
            }
        }
        public bool RenameCategory(string newCategoryName, int id)
        {
            bool status = false;
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {

                if (shoppingStoreContext.Catagories.Any(x => x.Name == newCategoryName))
                {
                    status = true;
                }
                Category category = shoppingStoreContext.Catagories.Find(id);

                // Edit DTO
                category.Name = newCategoryName;
                category.Description = newCategoryName.Replace(" ", "-").ToLower();

                // Save
                shoppingStoreContext.SaveChanges();
            }
            return status;
        }
        public bool AddProduct(Product product)
        {
            bool status = false;
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                product.Categories = new SelectList(shoppingStoreContext.Catagories.ToList(), "Id", "Name");
                if (shoppingStoreContext.Products.Any(x => x.Name == product.Name))
                {
                    product.Categories = new SelectList(shoppingStoreContext.Catagories.ToList(), "Id", "Name");
                    status = true;
                }
            }
            return status;

        }
        public Category SaveProduct(Product product)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                Category category = shoppingStoreContext.Catagories.FirstOrDefault(x => x.Id == product.CatagoryId);
                shoppingStoreContext.Products.Add(product);
                shoppingStoreContext.SaveChanges();
                return category;

            }
        }
        public Product UploadImage(int id)
        {

            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                Product product = shoppingStoreContext.Products.Find(id);
                shoppingStoreContext.SaveChanges();
                return product;
                // product.Categories = new SelectList(shoppingStoreContext.Catagories.ToList(), "Id", "Name");

            }

        }
        public List<Category> SelectListItem()
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Catagories.ToList();

            }
        }

        public List<Product> ListOfProduct(Product product, int ? categoryId)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {

                
           return   shoppingStoreContext.Products.ToArray()
                                  .Where(x => categoryId == null || categoryId == 0 || x.CatagoryId == categoryId)
                                  .Select(x => new Product(x))
                                .ToList();
            }
        }
       public  Product EditProduct(int id)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {

               return shoppingStoreContext.Products.Find(id);
            }
        }

        public bool CheckProduct(Product product, int id)

        {
            bool status=false;
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                if (shoppingStoreContext.Products.Where(x => x.Id != id).Any(x => x.Name == product.Name))
                {
                    status = true;
                }
                return status;
            }
        }
        public Product UpdateProduct(int id, Product product)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
               Product products = shoppingStoreContext.Products.Find(id);

                Category category = shoppingStoreContext.Catagories.FirstOrDefault(x => x.Id == product.CatagoryId);
                product.CatagoryName = category.Name;
                shoppingStoreContext.SaveChanges();

                return products;
            }

        }
        public Product FindProduct(int id)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
               Product product = shoppingStoreContext.Products.Find(id);
                shoppingStoreContext.SaveChanges();
                return product;
            }
        }
        public void DeleteProduct(int id)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                Product product = shoppingStoreContext.Products.Find(id);
                shoppingStoreContext.Products.Remove(product);
                shoppingStoreContext.SaveChanges();
            }
        }
        
    }
}

