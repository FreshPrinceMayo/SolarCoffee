using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;


namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        public static ProductModel SerializeProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreatedDate = product.CreatedDate,
                UpdatedTime = product.UpdatedTime,
                Price = product.Price,
                Description = product.Description,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable,
                Name = product.Name
            };
        }


        public static Product SerializeProductModel(ProductModel product)
        {
            return new Product
            {
                Id = product.Id,
                CreatedDate = product.CreatedDate,
                UpdatedTime = product.UpdatedTime,
                Price = product.Price,
                Description = product.Description,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable,
                Name = product.Name
            };
        }
    }

}
