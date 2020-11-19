using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;
using ShoppingStore.DAL;

namespace ShoppingStore.BL
{
    public interface IShopStoreBL
    {
        List<Category> ListOfCategory();
        int GetCategoryId(string name);
        List<Product> ListOfProducts(int categoryId);
        string GetCategoryName(int categoryId);
        bool ProductDetails(string name);

        Product GetProduct(string name);


    }
   public  class ShopStoreBL:IShopStoreBL
    {
        IShopStoreRepository shopStoreRepository;
        public ShopStoreBL()
        {
            shopStoreRepository = new ShopStoreRepository();
        }
       public List<Category> ListOfCategory()
        {
            return shopStoreRepository.ListOfCategory();
        }
        public int GetCategoryId(string name)
        {
            return shopStoreRepository.GetCategoryId(name); 
        }
        public List<Product> ListOfProducts(int categoryId)
        {
            return shopStoreRepository.ListOfProducts(categoryId);
        }
        public string GetCategoryName(int categoryId)
        {
            return shopStoreRepository.GetCategoryName(categoryId);
        }
        public bool ProductDetails(string name)
        {
            return shopStoreRepository.ProductDetails(name);
        }
       public Product GetProduct(string name)
        {
            return shopStoreRepository.GetProduct(name);
        }

    }
}
