namespace NaturalnieApp2.Main.Extensions.Database.Models
{
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.MVVM.Models.Inventory;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    internal static class ProductModelExtensions
    {
        public static InventoryModelDto ToInventory(this ProductModelDTO product)
        {
            return new InventoryModelDto()
            {
                ProductName = product.ProductName,
                BarCode = product.BarCode,
                BarCodeShort = product.BarCodeShort,
                Discount = product.Discount,
                CashRegisterProductId = product.CashRegisterProductId,
                CashRegisterProductName = product.CashRegisterProductName,
                FinalPrice = product.FinalPrice,
                ManufacturerName = product.ManufacturerName,
                Marigin = product.Marigin,
                PriceNet = product.PriceNet,
                PriceNetWithDiscount = product.PriceNetWithDiscount,
                SupplierCode = product.SupplierCode,
                TaxValue = product.Tax,
                SupplierName = product.SupplierName
            };
        }

        #region Public methods
        public static void FromDto( this ProductModel model, ProductModelDTO dto)
        {
            model.Supplier.Name = dto.SupplierName;
            model.ElzabProductId = dto.CashRegisterProductId;
            model.Manufacturer.Name = dto.ManufacturerName;
            model.ProductName = dto.ProductName;
            model.ElzabProductName = dto.CashRegisterProductName;
            model.PriceNet = dto.PriceNet;
            model.Discount = dto.Discount;
            model.PriceNetWithDiscount = dto.PriceNetWithDiscount;
            model.Tax.TaxValue = dto.Tax;
            model.Marigin = dto.Marigin;
            model.FinalPrice = dto.FinalPrice;
            model.BarCode = dto.BarCode;
            model.BarCodeShort = dto.BarCodeShort;
            model.SupplierCode = dto.SupplierCode;
            model.ProductInfo = dto.ProductInfo;
            model.CanBeRemoveFromCashRegister = dto.CanBeRemoveFromCashRegister;
        }

        public static ProductModelDTO ToDto(this ProductModel model)
        {
            ProductModelDTO dto = new();
            dto.Model = model;

            dto.SupplierName = model.Supplier.Name;
            dto.CashRegisterProductId = model.ElzabProductId;
            dto.ManufacturerName = model.Manufacturer.Name;
            dto.ProductName = model.ProductName;
            dto.CashRegisterProductName = model.ElzabProductName;
            dto.PriceNet = model.PriceNet;
            dto.Discount = model.Discount;
            dto.PriceNetWithDiscount = model.PriceNetWithDiscount;
            dto.Tax = model.Tax.TaxValue;
            dto.Marigin = model.Marigin;
            dto.FinalPrice = model.FinalPrice;
            dto.BarCode = model.BarCode;
            dto.BarCodeShort = model.BarCodeShort;
            dto.SupplierCode = model.SupplierCode;
            dto.ProductInfo = model.ProductInfo;
            dto.CanBeRemoveFromCashRegister = model.CanBeRemoveFromCashRegister;


            return dto;
        }

    }
}
#endregion