namespace NaturalnieApp2.Main.Extensions.Database.Models
{
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.MVVM.Models.Stock;
    using System;

    internal static class StockModelExtensions
    {

        public static void FromDto(this StockModel model, StockModelDto dto)
        {
            Func<ProductModel> newProduct = () =>
            {
                ProductModel product = new();
                product.FromDto(dto.Product);
                return product;
            };

            model.ActualQuantity = dto.ActualQuantity;
            model.LastQuantity = dto.LastQuantity;
            model.ModificationDate = dto.ModificationDate;
            model.ExpirationDate = dto.ExpirationDate;
            model.Product = dto.Model?.Product ?? newProduct();
        }

        public static StockModelDto ToDto(this StockModel model)
        {
            StockModelDto dto = new();
            dto.Model = model;  

            dto.ActualQuantity = model.ActualQuantity;
            dto.LastQuantity = model.LastQuantity;
            dto.ModificationDate = model.ModificationDate;
            dto.ExpirationDate = model.ExpirationDate;
            dto.Product = model.Product.ToDto();

            return dto;
        }
    }
}
