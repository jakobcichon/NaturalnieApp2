namespace NaturalnieApp2.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("product_changelog")]
    public class ProductChangelogModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int ElzabProductId { get; set; }
        public int ManufacturerId { get; set; }
        public string ProductName { get; set; }
        public string ElzabProductName { get; set; }
        public float PriceNet { get; set; }
        public int Discount { get; set; }
        public float PriceNetWithDiscount { get; set; }
        public int TaxId { get; set; }
        public int Marigin { get; set; }
        public float FinalPrice { get; set; }
        public string BarCode { get; set; }
        public string BarCodeShort { get; set; }
        public string SupplierCode { get; set; }
        public string ProductInfo { get; set; }
        public DateTime DateAndTime { get; set; }
        public string OperationType { get; set; }

    }
}
