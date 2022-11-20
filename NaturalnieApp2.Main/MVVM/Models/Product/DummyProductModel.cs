namespace NaturalnieApp2.Main.MVVM.Models.Product
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Attributes.ValidationRules;
    using NaturalnieApp2.Common.Binding;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Documents;

    public enum TestEnum
    {
        Test,
        kwiatek,
        Dziewczyna

    }

    internal record DummyProductModel: ModelBase
    {
        private string name;
        private double price;
        private int tax;
        private List<int> taxValuesProvider;
        private TestEnum options;
        private bool inCashRegister;

        [RegexStringValidatorCustom(@"\d")]
        [StringLengthCustom(5)]
        [DisplayableName("Nazwa produktu")]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [RangeCustom(2, 5.13)]
        [DisplayableName("Cena netto")]
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        [HasAdmissibleListAttribute("TaxValuesProvider")]
        [DisplayableName("Podatek")]
        public int Tax
        {
            get { return tax; }
            set { SetProperty(ref tax, value); }
        }

        [DoNotDisplay]
        public List<int> TaxValuesProvider
        {
            get { return taxValuesProvider; }
            set { SetProperty(ref taxValuesProvider, value); }
        }

        [DisplayableName("Opcje testowe")]
        public TestEnum Options
        {
            get { return options; }
            set { SetProperty(ref options, value); }
        }

        [DisplayableName("Obecny w kasie")]
        public bool InCashRegister
        {
            get { return inCashRegister; }
            set { SetProperty(ref inCashRegister, value); }
        }

    }
}
