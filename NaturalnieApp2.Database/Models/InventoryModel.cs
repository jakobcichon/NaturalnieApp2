namespace NaturalnieApp2.Database.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("inventory")]
    public class InventoryModel
    {
        [Key]
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public string PersonName { get; set; }
        public DateTime LastModificationDate { get; set; }
        public int ProductQuantity { get; set; }
        public string SupplierName { get; set; }
        public int ElzabProductId { get; set; }
        public string ManufacturerName { get; set; }
        public string ProductName { get; set; }
        public string ElzabProductName { get; set; }
        public float PriceNet { get; set; }
        public int Discount { get; set; }
        public float PriceNetWithDiscount { get; set; }
        public int TaxValue { get; set; }
        public int Marigin { get; set; }
        public float FinalPrice { get; set; }
        public string BarCode { get; set; }
        public string BarCodeShort { get; set; }
        public string SupplierCode { get; set; }
    }      
}
