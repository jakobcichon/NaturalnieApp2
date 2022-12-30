namespace NaturalnieApp2.Main.MVVM.Models.Inventory
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Attributes.ValidationRules;
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Model;
    using System;
    using System.ComponentModel.DataAnnotations;

    public record InventoryModelDTO : ValidatableBindableRecordBase, IModel, IConvertableModel<InventoryModel>
    {
        #region Fields
        private string inventoryName;
        private string personName;
        private DateTime lastModificationDate;
        private int productQuantity;
        private string supplierName;
        private int elzabProductId;
        private string manufacturerName;
        private string productName;
        private string elzabProductName;
        private float priceNet;
        private int discount;
        private float priceNetWithDiscount;
        private int taxValue;
        private int marigin;
        private float finalPrice;
        private string barCode;
        private string barCodeShort;
        private string supplierCode;
        #endregion

        public InventoryModelDTO()
        {

        }

        public InventoryModelDTO(InventoryModel model)
        {
            this.FromModel(model);
        }

        #region Properties
        [DoNotDisplay]
        public bool IsValid { get => this.HasErrors; }

        [DisplayableName("Tytuł inwetarza")]
        [StringLengthCustom(255)]
        [RegexStringValidatorCustom(@"^\S+$")]
        [RequiredCustom]
        public string InventoryName
        {
            get { return inventoryName; }
            set { SetProperty(ref inventoryName, value); }
        }

        [DisplayableName("Imię osoby")]
        [StringLengthCustom(255)]
        [RegexStringValidatorCustom(@"^\S+$")]
        [RequiredCustom]
        public string PersonName
        {
            get { return personName; }
            set { SetProperty(ref personName, value); }
        }

        [DisplayableName("Data ostatniej modyfikacji")]
        public DateTime LastModificationDate
        {
            get { return lastModificationDate; }
            set { SetProperty(ref lastModificationDate, value); }
        }

        [DisplayableName("Ilość")]
        public int ProductQuantity
        {
            get { return productQuantity; }
            set { SetProperty(ref productQuantity, value); }
        }

        [DisplayableName("Nazwa dostawcy")]
        public string SupplierName
        {
            get { return supplierName; }
            set { SetProperty(ref supplierName, value); }
        }

        [DisplayableName("Numer w kasie fiskalnej")]
        public int ElzabProductId
        {
            get { return elzabProductId; }
            set { SetProperty(ref elzabProductId, value); }
        }

        [DisplayableName("Nazwa producenta")]
        public string ManufacturerName
        {
            get { return manufacturerName; }
            set { SetProperty(ref manufacturerName, value); }
        }

        [DisplayableName("Nazwa produktu")]
        public string ProductName
        {
            get { return productName; }
            set { SetProperty(ref productName, value); }
        }

        [DisplayableName("Nazwa produktu w kasie fiskalnej")]
        public string ElzabProductName
        {
            get { return elzabProductName; }
            set { SetProperty(ref elzabProductName, value); }
        }

        [DisplayableName("Cena netto")]
        public float PriceNet
        {
            get { return priceNet; }
            set { SetProperty(ref priceNet, value); }
        }

        [DisplayableName("Rabat dostawcy")]
        public int Discount
        {
            get { return discount; }
            set { SetProperty(ref discount, value); }
        }

        [DisplayableName("Cena netto po rabacie")]
        public float PriceNetWithDiscount
        {
            get { return priceNetWithDiscount; }
            set { SetProperty(ref priceNetWithDiscount, value); }
        }

        [DisplayableName("Podatek")]
        public int TaxValue
        {
            get { return taxValue; }
            set { SetProperty(ref taxValue, value); }
        }

        [DisplayableName("Marża")]
        public int Marigin
        {
            get { return marigin; }
            set { SetProperty(ref marigin, value); }
        }

        [DisplayableName("Cena klineta")]
        public float FinalPrice
        {
            get { return finalPrice; }
            set { SetProperty(ref finalPrice, value); }
        }

        [DisplayableName("Kode kreskowy")]
        public string BarCode
        {
            get { return barCode; }
            set { SetProperty(ref barCode, value); }
        }

        [DisplayableName("Kode kreskowy wewnętrzny")]
        public string BarCodeShort
        {
            get { return barCodeShort; }
            set { SetProperty(ref barCodeShort, value); }
        }

        [DisplayableName("Kode dostawcy")]
        public string SupplierCode
        {
            get { return supplierCode; }
            set { SetProperty(ref supplierCode, value); }
        }
        #endregion


        #region Public methods
        public void FromModel(InventoryModel model)
        {
            this.InventoryName = model.InventoryName;
            this.PersonName = model.PersonName;
            this.LastModificationDate = model.LastModificationDate;
            this.ProductQuantity = model.ProductQuantity;
            this.SupplierName = model.SupplierName;
            this.ElzabProductId = model.ElzabProductId;
            this.ManufacturerName = model.ManufacturerName;
            this.ProductName = model.ProductName;
            this.ElzabProductName = model.ElzabProductName;
            this.PriceNet = model.PriceNet;
            this.Discount = model.Discount;
            this.PriceNetWithDiscount = model.PriceNetWithDiscount;
            this.TaxValue = model.TaxValue;
            this.Marigin = model.Marigin;
            this.FinalPrice = model.FinalPrice;
            this.BarCode = model.BarCode;
            this.BarCodeShort = model.BarCodeShort;
            this.SupplierCode = model.SupplierCode;
        }

        public InventoryModel ToModel()
        {
            InventoryModel model = new();

            model.InventoryName = this.InventoryName;
            model.PersonName = this.PersonName;
            model.LastModificationDate = this.LastModificationDate;
            model.ProductQuantity = this.ProductQuantity;
            model.SupplierName = this.SupplierName;
            model.ElzabProductId = this.ElzabProductId;
            model.ManufacturerName = this.ManufacturerName;
            model.ProductName = this.ProductName;
            model.ElzabProductName = this.ElzabProductName;
            model.PriceNet = this.PriceNet;
            model.Discount = this.Discount;
            model.PriceNetWithDiscount = this.PriceNetWithDiscount;
            model.TaxValue = this.TaxValue;
            model.Marigin = this.Marigin;
            model.FinalPrice = this.FinalPrice;
            model.BarCode = this.BarCode;
            model.BarCodeShort = this.BarCodeShort;
            model.SupplierCode = this.SupplierCode;

            return model;
        }
        #endregion
    }
}
