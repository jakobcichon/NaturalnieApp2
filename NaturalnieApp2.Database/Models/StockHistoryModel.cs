namespace NaturalnieApp2.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("stock_history")]
    public class StockHistoryModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAndTime { get; set; }
        public string OperationType { get; set; }
        public string SalesIdForAutomaticUpdate { get; set; }
    }
}
