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
        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                    .Include(x => x.Product)
                    .Where(x => !x.Product.IsArchived)
                    .ToList();
        }

        public ProductInventory GetProductId(int productId)
        {
            return _db.ProductInventories.FirstOrDefault(x => x.Product.Id == productId);
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;

            try
            {
                var inventory = _db.ProductInventories
               .Include(x => x.Product)
               .FirstOrDefault(x => x.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot();
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
    }
}
