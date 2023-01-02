namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Database.Interfaces;
    using NaturalnieApp2.Database.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class StockCommands: DbCommandBase, IStockCommands
    {
        public StockCommands(string connectionString) : base(connectionString)
        {

        }
        public async Task AddAsync(StockModel model)
        {

        }

        public async Task EditAsync(StockModel model)
        {

        }


        public async Task<ICollection<StockModel>> GetAllElementsAsync()
        {
            var result = await dbContext.Stock.ToListAsync();

            return result;
        }

        public async Task<StockModel> GetStockForProductAsync(ProductModel product)
        {
            var result = await dbContext.Stock.Where(s => s.Product.ProductName == product.ProductName).SingleOrDefaultAsync();

            return result;
        }
    }
}
