using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;
using ShoppingStore.DAL;

namespace ShoppingStore.BL
{
    public interface IShopBL
    {
      List< Category> Category();
       bool AddNewCategory(string categoryname,Category category);
        void ReorderCategories(int[] id);
       void DeleteCatagory(int id);
       bool RenameCategory(string newCategoryName, int id);
       bool AddProduct(Product product);
       Category SaveProduct(Product product);
       List<Category> SelectListItem();

        Product UploadImage(int id);
        List<Product> ListOfProduct(Product product,int ? categoryId);
        Product EditProduct(int id);
        bool CheckProduct(Product product,int id);
        Product UpdateProduct(int id, Product product);

        Product FindProduct(int id);
        void DeleteProduct(int id);
    }


    public class ShopBL:IShopBL
    {
      
        IShopRepository shopRepository;
        public ShopBL()
        {
            shopRepository = new ShopRepository();
        }
        public List<Category> Category()
        {
            return shopRepository.Category();
        }
        public bool AddNewCategory(string categoryname,Category category)
        {
            return shopRepository.AddNewCategory( categoryname,category);
        }
        public void ReorderCategories(int[] id)
        {
            shopRepository.ReorderCategories(id);
        }
       public void DeleteCatagory(int id)
        {
            shopRepository.DeleteCatagory(id);
        }
       public bool RenameCategory(string newCategoryName, int id)
        {
           return shopRepository.RenameCategory(newCategoryName, id);
        }
       public bool AddProduct(Product product)
        {
           return shopRepository.AddProduct(product);
        }
        public Category SaveProduct(Product product)
        {
           return shopRepository.SaveProduct(product);
        }
       public Product UploadImage(int id)
        {
            return shopRepository.UploadImage(id);
        }
       public List<Product> ListOfProduct(Product product,int ? categoryId)
        {
            return shopRepository.ListOfProduct(product,categoryId);
        }
        public List<Category> SelectListItem()
        {
            return shopRepository.SelectListItem();
        }
        public Product EditProduct(int id)
        {
            return shopRepository.EditProduct(id);
        }

        public bool CheckProduct(Product product, int id)
        {
            return shopRepository.CheckProduct(product,id);
        }
        public Product UpdateProduct(int id,Product product)
        {
            return shopRepository.UpdateProduct(id,product);
        }
        public Product FindProduct(int id)
        {
            return shopRepository.FindProduct(id);
        }
        public void DeleteProduct(int id)
        {
            shopRepository.DeleteProduct(id);
        }
        
    }
}
