namespace NaturalnieApp2.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("sales")]
    public class SaleModel
    {
        [Key]
        public int Id { get; set; }
        public int ElementType { get; set; }
        public DateTime TimeOfAdded { get; set; }
        public string EntryUniqueIdentifier { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }
        public string Attribute6 { get; set; }
        public string Attribute7 { get; set; }
        public string Attribute8 { get; set; }
        public string Attribute9 { get; set; }
        public string Attribute10 { get; set; }
        public string Attribute11 { get; set; }
        public string Attribute12 { get; set; }
        public string Attribute13 { get; set; }
        public string Attribute14 { get; set; }
        public string Attribute15 { get; set; }
        public string Attribute16 { get; set; }
        public string Attribute17 { get; set; }
    }
}
