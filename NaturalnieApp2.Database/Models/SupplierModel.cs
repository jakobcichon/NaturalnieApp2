namespace NaturalnieApp2.Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("supplier")]
    public class SupplierModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }

        public SupplierModel DeepCopy()
        {
            SupplierModel supplier = (SupplierModel)this.MemberwiseClone();
            supplier.Id = this.Id;
            supplier.Name = this.Name;
            supplier.Info = this.Info;

            return supplier;
        }
    }
}
