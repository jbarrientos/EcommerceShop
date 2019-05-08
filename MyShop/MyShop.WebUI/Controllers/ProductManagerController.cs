using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel vm = new ProductManagerViewModel
            {
                ProductCategories = productCategories.Collection().ToList(), 
                Product = new Product()
            };

            
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ProductManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request != null)
                {
                    HttpPostedFileBase fileToUpload = Request.Files["file"];
                    model.Product.Image = model.Product.Id + Path.GetExtension(fileToUpload.FileName);
                    fileToUpload.SaveAs(Server.MapPath("//Content//ProductImages//") + model.Product.Image);
                }
                context.Insert(model.Product);
                context.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                
                model.ProductCategories = productCategories.Collection().ToList();
                return View(model);
            }

            

        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);

            ProductManagerViewModel vm = new ProductManagerViewModel
            {
                Product = product,
                ProductCategories = productCategories.Collection().ToList()
            };

            if (product == null)
                return HttpNotFound();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel model)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();

            if (Request != null)
            {
                HttpPostedFileBase fileToUpload = Request.Files["file"];
                model.Product.Image = model.Product.Id + Path.GetExtension(fileToUpload.FileName);
                fileToUpload.SaveAs(Server.MapPath("//Content//ProductImages//") + model.Product.Image);
            }
            
            
            context.Update(model.Product);
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