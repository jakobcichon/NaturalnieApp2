namespace NaturalnieApp2.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("stock")]
    public class StockModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ActualQuantity { get; set; }
        public int LastQuantity { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string BarcodeWithDate { get; set; }
    }
}
