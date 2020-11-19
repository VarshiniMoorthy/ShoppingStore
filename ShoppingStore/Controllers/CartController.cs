using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingStore.Models;
using ShoppingStore.BL;
using ShoppingStore.Entity;
using System.Net.Mail;
using System.Net;

namespace ShoppingStore.Controllers
{
    public class CartController : Controller
    {
        static readonly log4net.ILog log =

      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Cart
        CartViewModel model;
        ICartBL cartBL;
        public CartController()
        {
            model = new CartViewModel();
            cartBL = new CartBL();
        }
        public ActionResult CheckCart()
        {
           
                // Init the cart list
                var cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();
            try
            {
                // Check if cart is empty
                if (cart.Count == 0 || Session["cart"] == null)
                {
                    ViewBag.Message = "Your cart is empty.";
                    return View();
                }

                // Calculate total and save to ViewBag

                decimal total = 0m;

                foreach (var item in cart)
                {
                    total += item.Total;
                }

                ViewBag.GrandTotal = total;
            }
            catch(Exception exception)
            {
                log.Error(exception.Message);
            }
            // Return view with list
            return View(cart);

        }
        public ActionResult CartQuantityAndPrice()
        {


            // Init quantity
            int quantity = 0;

            // Init price
            decimal price = 0m;
            try
            {
                // Check for cart session
                if (Session["cart"] != null)
                {
                    // Get total qty and price
                    var list = (List<CartViewModel>)Session["cart"];

                    foreach (var item in list)
                    {
                        quantity += item.Quantity;
                        price += item.Quantity * item.Price;
                    }

                    model.Quantity = quantity;
                    model.Price = price;

                }
                else
                {
                    // Or set qty and price to 0
                    model.Quantity = 0;
                    model.Price = 0m;
                }
            }
            catch(Exception exception)
            {
                log.Error(exception.Message);
            }
            // Return partial view with model
            return PartialView(model);

        }
        public ActionResult AddToCart(int id)
        {
            // Init CartVM list
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();

            // Init CartVM

            Product product = cartBL.AddToCart(id);

            // Check if the product is already in cart
            var productInCart = cart.FirstOrDefault(x => x.ProductId == id);
            try
            {
                // If not, add new
                if (product == null)
                {
                    cart.Add(new CartViewModel()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = 1,
                        Price = product.Price,
                        Image = product.ImageName
                    });
                }
                else
                {
                    // If it is, increment
                    productInCart.Quantity++;
                }


                // Get total qty and price and add to model

                int quantity = 0;
                decimal price = 0m;

                foreach (var item in cart)
                {
                    quantity += item.Quantity;
                    price += item.Quantity * item.Price;
                }

                model.Quantity = quantity;
                model.Price = price;

                // Save cart back to session
                Session["cart"] = cart;
            }
            catch(Exception exception)
            {
                log.Error(exception.Message);
            }
            // Return partial view with model
            return PartialView(model);
        }
        public JsonResult IncrementProduct(int productId)
        {
            
            
                // Init cart list
                List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;

                // Get cartVM from list
                model = cart.FirstOrDefault(x => x.ProductId == productId);

                // Increment qty
                model.Quantity++;

                // Store needed data
                var result = new { qty = model.Quantity, price = model.Price };
           

            // Return json with data
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DecrementProduct(int productId)
        {
            // Init cart
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;


            // Get model from list
            model = cart.FirstOrDefault(x => x.ProductId == productId);

            // Decrement qty
            if (model.Quantity > 1)
            {
                model.Quantity--;
            }
            else
            {
                model.Quantity = 0;
                cart.Remove(model);
            }

            // Store needed data
            var result = new { qty = model.Quantity, price = model.Price };

            // Return json
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: /Cart/RemoveProduct
        public void RemoveProduct(int productId)
        {
            try {
                // Init cart list
                List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;


                // Get model from list
                model = cart.FirstOrDefault(x => x.ProductId == productId);

                // Remove model from list
                cart.Remove(model);
            }
            catch (Exception exception)
            {
                log.Error(exception.Message);
            }
        }


        public ActionResult PaypalPartial()
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;
            return PartialView(cart);
        }

        public void PlaceOrder()
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;

            string username = User.Identity.Name;

            int orderId = 0;
            // Init OrderDTO
            Order order = new Order();

            // Get user id
            var user = cartBL.PlaceOrder(username);
            int userId = user.Id;

            // Add to OrderDTO and save
            order.UserId = userId;
            order.CreatedAt = DateTime.Now;


            // Get inserted id
            orderId = order.OrderId;

            // Init OrderDetailsDTO
            OrderDetails orderDetails = new OrderDetails();

            // Add to OrderDetailsDTO
            foreach (var item in cart)
            {
                orderDetails.OrderId = orderId;
                orderDetails.UserId = userId;
                orderDetails.ProductId = item.ProductId;
                orderDetails.Quantity = item.Quantity;
                cartBL.SaveOrderDetails(orderDetails);

            }


            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("383bd05667a3c9", "8cd37ad740503e"),
                EnableSsl = true
            };
            client.Send("admin@example.com", "admin@example.com", "New Order", "You have a new order. Order number " + orderId);
            // return View();
            Session["cart"] = null;
        }
    }
}
