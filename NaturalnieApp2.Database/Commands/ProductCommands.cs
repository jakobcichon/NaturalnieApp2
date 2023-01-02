namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Database.Interfaces;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class ProductCommands : DbCommandBase, IProductCommands
    {
        public ProductCommands(string connectionString) : base(connectionString)
        {

        }

        public async Task AddAsync(ProductModel model)
        {

        }

        public async Task EditAsync(ProductModel model)
        {
            dbContext.Products.Add(model);
            dbContext.Entry(model).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ProductModel>> GetAllElementsAsync()
        {
            var products = await dbContext.Products.ToListAsync();

            return products;
        }

        public async Task<ProductModel> GetProductForBarcode(string barcode)
        {
            var products = await dbContext.Products.
                Where(p => p.BarCode == barcode || p.BarCodeShort == barcode).
                SingleOrDefaultAsync();

            return products;
        }
    }
}
