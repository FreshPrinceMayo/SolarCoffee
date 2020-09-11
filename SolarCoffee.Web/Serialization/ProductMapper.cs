using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;


namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        public static ProductViewModel SerializeProductModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                CreatedDate = product.CreatedDate,
                UpdatedTime = product.UpdatedDate,
                Price = product.Price,
                Description = product.Description,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable,
                Name = product.Name
            };
        }


        public static Product SerializeProductModel(ProductViewModel product)
        {
            return new Product
            {
                Id = product.Id,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedTime,
                Price = product.Price,
                Description = product.Description,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable,
                Name = product.Name
            };
        }
    }

}
