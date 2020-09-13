using SolarCoffee.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;
        public ProductService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.Products.Add(product);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = "Product archived",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    Data = null
                };

            }

        }

        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = false,
                    Time = DateTime.UtcNow,
                    Message = $"Error saving new product {product.Name}",
                    Data = product
                };
            }

        }

        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
