namespace NaturalnieApp2.Main.Extensions.Database.Models
{
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.MVVM.Models.Inventory;

    public static class InventoryModelExtensions
    {
        public static InventoryModelDto ToDto(this InventoryModel model)
        {

            InventoryModelDto dto = new()
            {
                Model = model,

                InventoryName = model.InventoryName,
                PersonName = model.PersonName,
                LastModificationDate = model.LastModificationDate,
                ProductQuantity = model.ProductQuantity,
                SupplierName = model.SupplierName,
                CashRegisterProductId = model.ElzabProductId,
                ManufacturerName = model.ManufacturerName,
                ProductName = model.ProductName,
                CashRegisterProductName = model.ElzabProductName,
                PriceNet = model.PriceNet,
                Discount = model.Discount,
                PriceNetWithDiscount = model.PriceNetWithDiscount,
                TaxValue = model.TaxValue,
                Marigin = model.Marigin,
                FinalPrice = model.FinalPrice,
                BarCode = model.BarCode,
                BarCodeShort = model.BarCodeShort,
                SupplierCode = model.SupplierCode
            };

            return dto;
        }

        public static void FromDto(this InventoryModel model, InventoryModelDto dto)
        {
            model.InventoryName = dto.InventoryName;
            model.PersonName = dto.PersonName;
            model.LastModificationDate = dto.LastModificationDate;
            model.ProductQuantity = dto.ProductQuantity;
            model.SupplierName = dto.SupplierName;
            model.ElzabProductId = dto.CashRegisterProductId;
            model.ManufacturerName = dto.ManufacturerName;
            model.ProductName = dto.ProductName;
            model.ElzabProductName = dto.CashRegisterProductName;
            model.PriceNet = dto.PriceNet;
            model.Discount = dto.Discount;
            model.PriceNetWithDiscount = dto.PriceNetWithDiscount;
            model.TaxValue = dto.TaxValue;
            model.Marigin = dto.Marigin;
            model.FinalPrice = dto.FinalPrice;
            model.BarCode = dto.BarCode;
            model.BarCodeShort = dto.BarCodeShort;
            model.SupplierCode = dto.SupplierCode;
        }
    }
}
