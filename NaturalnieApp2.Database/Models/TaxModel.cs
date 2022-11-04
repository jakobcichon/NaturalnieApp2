namespace NaturalnieApp2.Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tax")]
    public class TaxModel
    {
        [Key]
        public int Id { get; set; }
        public int TaxValue { get; set; }

        public ICollection<ProductModel> Products { get; set; }

        public TaxModel DeepCopy()
        {
            TaxModel tax = (TaxModel)this.MemberwiseClone();
            tax.Id = this.Id;
            tax.TaxValue = this.TaxValue;

            return tax;
        }
    }
}
