using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (product != null)
            {
                product.Title = obj.Title;
                product.ISBN = obj.ISBN;
                product.Price = obj.Price;
                product.Price50 = obj.Price50;
                product.ListPrice = obj.ListPrice;
                product.Price100 = obj.Price100;
                product.Description = obj.Description;
                product.CategoryId = obj.CategoryId;
                product.Author = obj.Author;
                product.ProductImages = obj.ProductImages;
                //if (product.ImageUrl != null)
                //{
                //    product.ImageUrl = obj.ImageUrl;
                //}
            }
        }
    }
}
