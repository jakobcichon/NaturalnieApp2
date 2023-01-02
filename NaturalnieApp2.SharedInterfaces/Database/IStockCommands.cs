namespace NaturalnieApp2.Database.Interfaces
{
    using NaturalnieApp2.SharedInterfaces.Database;
    using System.Threading.Tasks;


    public interface IStockCommands<T, Tproduct> : IDatabaseGeneralCommands<T>
    {
        Task<T> GetStockForProductAsync(Tproduct product);
    }
}
