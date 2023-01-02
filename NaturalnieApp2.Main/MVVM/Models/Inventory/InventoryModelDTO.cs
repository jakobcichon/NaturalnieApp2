namespace NaturalnieApp2.Main.MVVM.Models.Inventory
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Attributes.ValidationRules;
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Model;
    using System;
    using System.ComponentModel.DataAnnotations;

    public record InventoryModelDto : ValidatableBindableRecordBase, IModelDto<InventoryModel>
    {
        #region Fields
        private string inventoryName;
        private string personName;
        private DateTime lastModificationDate;
        private int productQuantity;
        private string supplierName;
        private int? cashRegisterProductId;
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

        public InventoryModelDto()
        {

        }

        #region Properties
        [DoNotDisplay]
        public InventoryModel? Model { get; set; }

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
        public int? CashRegisterProductId
        {
            get { return cashRegisterProductId; }
            set { SetProperty(ref cashRegisterProductId, value); }
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
        public string CashRegisterProductName
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

    }
}
