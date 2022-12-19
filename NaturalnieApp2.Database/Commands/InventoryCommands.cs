namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class InventoryCommands : CommandBase, IDatabaseGeneralCommands<InventoryModel>
    {
        public InventoryCommands(string connectionString) : base(connectionString)
        {

        }

        public async Task<ICollection<InventoryModel>> GetAllElementsAsync()
        {
            var result = await dbContext.Inventory.ToListAsync();

            return result;
        }
    }
}
