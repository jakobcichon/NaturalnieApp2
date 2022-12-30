namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Database.Interfaces;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class InventoryCommands : DbCommandBase, IInventoryCommands
    {
        public InventoryCommands(string connectionString) : base(connectionString)
        {

        }

        public async Task<ICollection<InventoryModel>> GetAllElementsAsync()
        {
            var result = await dbContext.Inventory.ToListAsync();

            return result;
        }

        public async Task<ICollection<string>> GetInventoriesNamesAsync()
        {
            var result = await dbContext.Inventory.Select(i => i.InventoryName)
                .Distinct()
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<string>> GetPersonNamesForGivenInventoryAsync(string inventoryName)
        {
            var result = await dbContext.Inventory.Where(i => i.InventoryName.Equals(inventoryName, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.PersonName)
                .Distinct()
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<InventoryModel>> GetEntriesForGivenInventoryAsync(string inventoryName)
        {
            var result = await dbContext.Inventory
                .Where(i => i.InventoryName.Equals(inventoryName, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<InventoryModel>> GetEntriesForGivenInventoryAndPersonAsync(string inventoryName, string personName)
        {
            var result = await dbContext.Inventory
                .Where(i => i.InventoryName.Equals(inventoryName, StringComparison.InvariantCultureIgnoreCase))
                .Where(i => i.PersonName.Equals(personName, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            return result;
        }
    }
}
