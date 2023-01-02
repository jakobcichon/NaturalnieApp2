namespace NaturalnieApp2.Main.MVVM.Models.Stock
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Model;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using System;

    internal record StockModelDto : ValidatableBindableRecordBase, IModelDto<StockModel>
    {

        #region Fields
        private ProductModelDTO product;
        private int actualQuantity;
        private int lastQuantity;
        private DateTime modificationDate;
        private DateTime expirationDate;
        #endregion
    

        #region Properties
        [DoNotDisplay]
        public StockModel? Model { get; set; }

        [DoNotDisplay]
        public bool IsValid { get => this.HasErrors; }

        public ProductModelDTO Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
        }

        public int ActualQuantity
        {
            get { return actualQuantity; }
            set { SetProperty(ref actualQuantity, value); }
        }

        public int LastQuantity
        {
            get { return lastQuantity; }
            set { SetProperty(ref lastQuantity, value); }
        }

        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { SetProperty(ref modificationDate, value); }
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { SetProperty(ref expirationDate, value); }
        } 
        #endregion
    }
}
