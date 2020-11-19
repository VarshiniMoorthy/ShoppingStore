using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;
namespace ShoppingStore.DAL
{
    public interface IShopStoreRepository
    {
        List<Category> ListOfCategory();
         int GetCategoryId(string name);
        List<Product> ListOfProducts(int categoryId);
        string GetCategoryName(int categoryId);
        bool ProductDetails(string name);
        Product GetProduct(string name);



    }
    public class ShopStoreRepository:IShopStoreRepository
    {
        public List<Category> ListOfCategory()
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
               List<Category> categories =  shoppingStoreContext.Catagories.ToArray().OrderBy(x => x.Sorting).Select(x => new Category(x)).ToList();

                return categories;
            }
        }
        public int GetCategoryId(string name)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                Category category =shoppingStoreContext.Catagories.Where(x => x.Description == name).FirstOrDefault();
                return category.Id;
            }

        }
        public List<Product> ListOfProducts(int categoryId)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                List<Product> products = shoppingStoreContext.Products.ToArray().Where(x => x.CatagoryId == categoryId).Select(x => new Product(x)).ToList();
                return products;
            }
        }
        public string GetCategoryName(int categoryId)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                Product product = shoppingStoreContext.Products.Where(x => x.CatagoryId == categoryId).FirstOrDefault();
                return product.CatagoryName;
            }

        }
        public bool ProductDetails(string name)
        {
            bool status = false;
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                if (!shoppingStoreContext.Products.Any(x => x.Slug.Equals(name)))
                {
                    status = true;
                }
                return status;
            }
        }
        public Product GetProduct(string name)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
               Product product = shoppingStoreContext.Products.Where(x => x.Slug == name).FirstOrDefault();
                return product;
            }
        }

    }
}
