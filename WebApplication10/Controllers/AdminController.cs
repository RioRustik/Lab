using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        private ItemContext itemDB = new ItemContext();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Orders()
        {
            return View(itemDB.Details);
        }

        [HttpGet]
        public ActionResult Items()
        {
            return View(itemDB.Items);
        }

        [HttpPost]
        public ViewResult Items(int id)
        {
            int idItem = Convert.ToInt32(Request.Form["id"]);
            int quantity = Convert.ToInt32(Request.Form["quant"]);
            Item item = itemDB.Items.Find(idItem);
            item.Quantity = quantity;
            itemDB.SaveChanges();
            return View(itemDB.Items);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Add(Item item)
        {
            itemDB.Items.Add(item);
            itemDB.SaveChanges();
            ViewBag.done = "Товар был успешно добавлен";
            return View();
        }
        public RedirectResult Out()
        {
            FormsAuthentication.SignOut();
            return RedirectPermanent("/Home/Index");
        }

    }
}