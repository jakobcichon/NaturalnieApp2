namespace NaturalnieApp2.Database.Interfaces
{
    using NaturalnieApp2.SharedInterfaces.Database;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IInventoryCommands<T> : IDatabaseGeneralCommands<T>
    {
        Task<ICollection<string>> GetInventoriesNamesAsync();
        Task<ICollection<string>> GetPersonNamesForGivenInventoryAsync(string inventoryName);
        Task<ICollection<T>> GetEntriesForGivenInventoryAsync(string inventoryName);
        Task<ICollection<T>> GetEntriesForGivenInventoryAndPersonAsync(string inventoryName, string personName);
    }
}
