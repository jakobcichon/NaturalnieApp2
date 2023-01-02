namespace NaturalnieApp2.Main.MVVM.Models.Product
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Attributes.ValidationRules;
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Model;
    using System.Collections.Generic;
    using System.Windows.Documents;

    public record ProductModelDTO : ValidatableBindableRecordBase, IModelDto<ProductModel>
    {
        #region Fields
        private int tax;
        private int marigin;
        private int discount;
        private float priceNet;
        private string barCode;
        private float finalPrice;
        private string supplierName;
        private string productInfo;
        private string productName;
        private string barCodeShort;
        private string supplierCode;
        private int? elzabProductId;
        private string manufacturerName;
        private float priceNetWithDiscount;
        private string cashRegisterProductName;
        private bool canBeRemoveFromCashRegister;
        #endregion

        [DoNotDisplay]
        public ProductModel? Model { get; set; }

        #region Properties
        [DoNotDisplay]
        public bool IsValid { get => this.HasErrors; }

        [DisplayableName("Nazwa dostawcy")]
        public string SupplierName
        {
            get { return supplierName; }
            set { SetProperty(ref supplierName, value); }
        }

        [DisplayableName("Numer w kasie fiskalnej")]
        public int? CashRegisterProductId
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

        [DisplayableName("Nazwa w kasie fiskalnej")]
        [StringLengthCustom(ProductNameLength)]
        public string CashRegisterProductName
        {
            get { return cashRegisterProductName; }
            set { SetProperty(ref cashRegisterProductName, value); }
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
        [HasAdmissibleList("TaxOptions")]
        public int Tax
        {
            get { return tax; }
            set { SetProperty(ref tax, value); }
        }

        [DoNotDisplay]
        public static List<int> TaxOptions
        {
            get { return new() { 0, 5, 8, 23 }; }
        }

        [DisplayableName("Marża")]
        public int Marigin
        {
            get { return marigin; }
            set { SetProperty(ref marigin, value); }
        }

        [DisplayableName("Cenna klienta")]
        public float FinalPrice
        {
            get { return finalPrice; }
            set { SetProperty(ref finalPrice, value); }
        }

        [DisplayableName("Kod kreskowy")]
        public string BarCode
        {
            get { return barCode; }
            set { SetProperty(ref barCode, value); }
        }

        [DisplayableName("Kod kreskowy wewnętrzny")]
        public string BarCodeShort
        {
            get { return barCodeShort; }
            set { SetProperty(ref barCodeShort, value); }
        }

        [DisplayableName("Kod dostawcy")]
        public string SupplierCode
        {
            get { return supplierCode; }
            set { SetProperty(ref supplierCode, value); }
        }

        [DisplayableName("Informacje o produkcie")]
        public string ProductInfo
        {
            get { return productInfo; }
            set { SetProperty(ref productInfo, value); }
        }

        [DisplayableName("Obecny w kasie fiskalnej")]
        public bool CanBeRemoveFromCashRegister
        {
            get { return canBeRemoveFromCashRegister; }
            set { SetProperty(ref canBeRemoveFromCashRegister, value); }
        }

        #endregion

        #region Public methods
      /*  public void FromModel(ProductModel model)
        {
            ModelRef = model;

            this.SupplierName = model.Supplier.Name;
            this.CashRegisterProductId = model.ElzabProductId;
            this.ManufacturerName = model.Manufacturer.Name;
            this.ProductName = model.ProductName;
            this.CashRegisterProductName = model.ElzabProductName;
            this.PriceNet = model.PriceNet;
            this.Discount = model.Discount;
            this.PriceNetWithDiscount = model.PriceNetWithDiscount;
            this.Tax = model.Tax.TaxValue;
            this.Marigin = model.Marigin;
            this.FinalPrice = model.FinalPrice;
            this.BarCode = model.BarCode;
            this.BarCodeShort = model.BarCodeShort;
            this.SupplierCode = model.SupplierCode;
            this.ProductInfo = model.ProductInfo;
            this.CanBeRemoveFromCashRegister = model.CanBeRemoveFromCashRegister;
        }

        public ProductModel ToModel()
        {
            ProductModel model = new ProductModel();

            //model.Supplier.Name = this.SupplierName;
            model.ElzabProductId = this.CashRegisterProductId;
            //model.Manufacturer.Name = this.ManufacturerName;
            model.ProductName = this.ProductName;
            model.ElzabProductName = this.CashRegisterProductName;
            model.PriceNet = this.PriceNet;
            model.Discount = this.Discount;
            model.PriceNetWithDiscount = this.PriceNetWithDiscount;
            //model.Tax.TaxValue = this.Tax;
            model.Marigin = this.Marigin;
            model.FinalPrice = this.FinalPrice;
            model.BarCode = this.BarCode;
            model.BarCodeShort = this.BarCodeShort;
            model.SupplierCode = this.SupplierCode;
            model.ProductInfo = this.ProductInfo;
            model.CanBeRemoveFromCashRegister = this.CanBeRemoveFromCashRegister;

            return model;
        } */
        #endregion
    }
}
