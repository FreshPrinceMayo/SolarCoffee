using SolarCoffee.Data.Models;
using System.Collections.Generic;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetProductById(int productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
