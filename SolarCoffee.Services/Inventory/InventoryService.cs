using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(ILogger<InventoryService> logger, SolarDbContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }


        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                    .Include(x => x.Product)
                    .Where(x => !x.Product.IsArchived)
                    .ToList();
        }

        public ProductInventory GetProductById(int productId)
        {
            return _db.ProductInventories.FirstOrDefault(x => x.Product.Id == productId);
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow.AddHours(-6);

            return _db.ProductInventorySnapshots
                .Include(x => x.Product)
                .Where(x => !x.Product.IsArchived && x.SnapShotTime > earliest)
                .ToList();
        }


        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;

            try
            {
                var inventory = _db.ProductInventories
               .Include(x => x.Product)
               .FirstOrDefault(x => x.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error creating inventory snapshot");
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Inventory updated {adjustment}",
                    Time = now
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Inventory update failed",
                    Time = now
                };
            }
        }

        private void CreateSnapshot(ProductInventory productInventory)
        {
            var now = DateTime.UtcNow;

            var snapshot = new ProductInventorySnapshot
            {
                Product = productInventory.Product,
                QuantityOnHand = productInventory.QuantityOnHand,
                SnapShotTime = now,
            };

            _db.ProductInventorySnapshots.Add(snapshot);
        }
    }
}
