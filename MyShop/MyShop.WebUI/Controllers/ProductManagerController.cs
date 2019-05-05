using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();

            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            context.Insert(product);
            context.Commit();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();
            
            context.Update(product);
            context.Commit();

            return RedirectToAction("Index");


        }

        public ActionResult Delete(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
                return HttpNotFound();

            
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
                return HttpNotFound();

            context.Delete(Id);
            context.Commit();

            return RedirectToAction("Index");
        }
    }
}