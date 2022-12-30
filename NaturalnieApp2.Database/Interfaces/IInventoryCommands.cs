namespace NaturalnieApp2.Database.Interfaces
{
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IInventoryCommands: IDatabaseGeneralCommands<InventoryModel>
    {
        Task<ICollection<string>> GetInventoriesNamesAsync();
        Task<ICollection<string>> GetPersonNamesForGivenInventoryAsync(string inventoryName);
        Task<ICollection<InventoryModel>> GetEntriesForGivenInventoryAsync(string inventoryName);
        Task<ICollection<InventoryModel>> GetEntriesForGivenInventoryAndPersonAsync(string inventoryName, string personName);
    }
}
