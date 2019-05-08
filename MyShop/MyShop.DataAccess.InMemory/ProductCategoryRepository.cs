using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            this.productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
                this.productCategories = new List<ProductCategory>();
        }

        public void Commit()
        {
            cache["productCategories"] = this.productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory categoryToUpdate = productCategories.Find(f => f.Id == category.Id);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
            }
            else
            {
                throw new Exception("Category not found.");
            }
        }

        public ProductCategory Find(string Id)
        {

            ProductCategory productCategory = productCategories.Find(f => f.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Category not found.");
            }

        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory category = productCategories.Find(f => f.Id == Id);

            if (category != null)
            {
                productCategories.Remove(category);
            }
            else
            {
                throw new Exception("Category not found.");
            }
        }
    }
}
