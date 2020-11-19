using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.DAL;
using ShoppingStore.Entity;

namespace ShoppingStore.BL
{
    public interface ICartBL
    {
        Product AddToCart(int id);
        User PlaceOrder(string userName);
        void SavePlaceOrder(Order order);
        void SaveOrderDetails(OrderDetails order);
    }

    public class CartBL : ICartBL
    {
        ICartRepository cartRepository;
        public CartBL()
        {
            cartRepository = new CartRepository();
        }
        public Product AddToCart(int id)
        {
            return cartRepository.AddToCart(id);
        }
       public User PlaceOrder(string userName)
        {
            return cartRepository.PlaceOrder(userName);
        }
        public void SavePlaceOrder(Order order)
        {
            cartRepository.SavePlaceOrder(order);
        }
       public void SaveOrderDetails(OrderDetails order)
        {
            cartRepository.SaveOrderDetails(order);
        }

    }
}