namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class ProductCommands : CommandBase, IDatabaseGeneralCommands<ProductModel>
    {
        public ProductCommands(string connectionString) : base(connectionString)
        {

        }

        public async Task<ICollection<ProductModel>> GetAllElementsAsync()
        {
            var products = await dbContext.Products.ToListAsync();

            return products;
        }
    }
}
