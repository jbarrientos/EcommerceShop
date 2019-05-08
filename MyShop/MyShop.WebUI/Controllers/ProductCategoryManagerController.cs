using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            context = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = 
                context.Collection().ToList();

            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();

            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }

            context.Insert(productCategory);
            context.Commit();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);

            if (productCategory == null)
                return HttpNotFound();

            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();

            context.Update(productCategory);
            context.Commit();

            return RedirectToAction("Index");


        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategory = context.Find(Id);

            if (productCategory == null)
                return HttpNotFound();


            return View(productCategory);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategory = context.Find(Id);

            if (productCategory == null)
                return HttpNotFound();

            context.Delete(Id);
            context.Commit();

            return RedirectToAction("Index");
        }
    }
}