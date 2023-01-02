namespace NaturalnieApp2.Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("product")]
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public virtual SupplierModel Supplier { get; set; } = null!;
        public int? ElzabProductId { get; set; }
        public int ManufacturerId { get; set; }
        public virtual ManufacturerModel Manufacturer { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ElzabProductName { get; set; } = null!;
        public float PriceNet { get; set; }
        public int Discount { get; set; }
        public float PriceNetWithDiscount { get; set; }
        public int TaxId { get; set; }
        public virtual TaxModel Tax { get; set; } = null!;
        public int Marigin { get; set; }
        public float FinalPrice { get; set; }
        public string BarCode { get; set; } = null!;
        public string BarCodeShort { get; set; } = null!;
        public string SupplierCode { get; set; } = null!;
        public string ProductInfo { get; set; } = null!;
        public bool CanBeRemoveFromCashRegister { get; set; }


        public ProductModel DeepCopy()
        {
            ProductModel product = (ProductModel)this.MemberwiseClone();
            product.Id = this.Id;
            product.SupplierId = this.SupplierId;
            product.ElzabProductId = this.ElzabProductId;
            product.ManufacturerId = this.ManufacturerId;
            product.ProductName = this.ProductName;
            product.ElzabProductName = this.ElzabProductName;
            product.PriceNet = this.PriceNet;
            product.Discount = this.Discount;
            product.PriceNetWithDiscount = this.PriceNetWithDiscount;
            product.Tax = this.Tax;
            product.Marigin = this.Marigin;
            product.FinalPrice = this.FinalPrice;
            product.BarCode = this.BarCode;
            product.BarCodeShort = this.BarCodeShort;
            product.SupplierCode = this.SupplierCode;
            product.ProductInfo = this.ProductInfo;
            product.CanBeRemoveFromCashRegister = this.CanBeRemoveFromCashRegister;
            return product;
        }
    }

    public enum ProductOperationType
    {
        AddNew,
        Update,
        Delete
    }
    public enum StockOperationType
    {
        AddNew,
        Update,
        Delete,
        AutomaticUpdate,
        AutomaticAddedNew,
        InventoryData
    }

}