namespace NaturalnieApp2.Database.Interfaces
{
    using NaturalnieApp2.SharedInterfaces.Database;
    using System.Threading.Tasks;


    public interface IProductCommands<T> : IDatabaseGeneralCommands<T>
    {
        Task<T> GetProductForBarcode(string barcode);
    }
}
