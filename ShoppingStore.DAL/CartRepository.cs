using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Entity;

namespace ShoppingStore.DAL

{
    public interface ICartRepository
    {
        Product AddToCart(int id);
        User PlaceOrder(string userName);
        void SavePlaceOrder(Order order);
        void SaveOrderDetails(OrderDetails order);



    }
    public class CartRepository : ICartRepository
    {
        ICartRepository cartRepository;
        public CartRepository()
        {
            cartRepository = new CartRepository();
        }
        public Product AddToCart(int id)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                // Get the product
                return shoppingStoreContext.Products.Find(id);

            }

        }
        public User PlaceOrder(string userName)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                return shoppingStoreContext.Users.FirstOrDefault(x => x.UserName == userName);

            }


        }
        public void SavePlaceOrder(Order order)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                shoppingStoreContext.Orders.Add(order);

                shoppingStoreContext.SaveChanges();


            }
        }
        public void SaveOrderDetails(OrderDetails order)
        {
            using (ShoppingStoreContext shoppingStoreContext = new ShoppingStoreContext())
            {
                shoppingStoreContext.OrderDetails.Add(order);

                shoppingStoreContext.SaveChanges();
            }

        }
    }
}

