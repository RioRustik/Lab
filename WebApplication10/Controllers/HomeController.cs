using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        private ItemContext itemDB = new ItemContext();
        private Cart cart = new Cart();
        [HttpGet]
        public ActionResult Index()
        {
            Session["Cart"] = cart;
            return View(itemDB.Items);
        }

        [HttpPost]
        public ViewResult Index(int id)
        {
            int idItem = Convert.ToInt32(Request.Form["id"]);
            int quantity = Convert.ToInt32(Request.Form["quantity"]);
            if (quantity > Convert.ToInt32(Request.Form["quant"]))
            {
                ViewBag.Quantity = "Вы не можете добавить больше товаров, чем есть на складе";
                return View(itemDB.Items);
            }
            Item item = itemDB.Items.Find(idItem);
            Detail detail = new Detail();
            detail.Id = idItem;
            detail.Title = item.Title;
            detail.Price = item.Price;
            detail.Quantity = quantity;
            Cart cart = (Cart)Session["Cart"];
            cart.Add(detail);
            Session["Cart"] = cart;
            ViewBag.Done = "Товар(ы) «" + item.Title + "» успешно добавлен(ы) в корзину";
            return View(itemDB.Items);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            bool result = FormsAuthentication.Authenticate(admin.UserName, admin.Password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(admin.UserName, true);
                return RedirectPermanent("/Admin/Index");
            }
            else
            {
                ViewBag.result = "Неверный логин или пароль";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Cart()
        {
            Cart cart = (Cart)Session["Cart"];
            return View(cart.Details);
        }
        [HttpPost]
        public ViewResult Cart(int id)
        {
            int idItem = Convert.ToInt32(Request.Form["id"]);
            Cart cart = (Cart)Session["Cart"];
            cart.Remove(idItem);
            return View(cart.Details);
        }
        [HttpGet]
        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Order(Customer customer)
        {
            Cart cart = (Cart)Session["Cart"];
            if (ModelState.IsValid)
            {
                itemDB.Customers.Add(customer);
                itemDB.SaveChanges();
                Order order = new Order();
                order.CustomerId = customer.Id;
                itemDB.Orders.Add(order);
                itemDB.SaveChanges();
                foreach (Detail d in cart.Details)
                {
                    d.OrderId = order.Id;
                    itemDB.Details.Add(d);
                    itemDB.SaveChanges();
                }
                return View("Thanks", order);
            }
            else
                return View();
            
        }

       
    }
}