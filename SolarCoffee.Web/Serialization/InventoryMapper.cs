using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System.Collections.Generic;

namespace SolarCoffee.Web.Serialization
{
    public static class InventoryMapper
    {
        public static List<ProductInventoryViewModel> SerializeInventory(List<ProductInventory> productInventoryList)
        {
            var viewModel = new List<ProductInventoryViewModel>();

            foreach (var productInventory in productInventoryList)
            {
                viewModel.Add(new ProductInventoryViewModel
                {
                    Id = productInventory.Id,
                    IdealQuantity = productInventory.IdealQuantity,
                    Product = ProductMapper.SerializeProductModel(productInventory.Product),
                    QuantityOnHand = productInventory.QuantityOnHand
                });
            }

            return viewModel;
        }
    }
}
