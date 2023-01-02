namespace NaturalnieApp2.Database.Interfaces
{
    using NaturalnieApp2.Database.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStockCommands : IStockCommands<StockModel, ProductModel>
    {
    }
}
