namespace NaturalnieApp2.Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("manufacturer")]
    public class ManufacturerModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarcodeEanPrefix { get; set; }
        public string Info { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }

        public ManufacturerModel DeepCopy()
        {
            ManufacturerModel manufacturer = (ManufacturerModel)this.MemberwiseClone();
            manufacturer.Id = this.Id;
            manufacturer.Name = this.Name;
            manufacturer.BarcodeEanPrefix = this.BarcodeEanPrefix;
            manufacturer.Info = this.Info;

            return manufacturer;
        }
    }
}
