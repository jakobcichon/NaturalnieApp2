namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Database.Interfaces;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class InventoryCommands : CommandBase, IInventoryCommands
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
            var result = await dbContext.Inventory.Select(i => i.InventoryName).Distinct().ToListAsync();

            return result;
        }
    }
}
