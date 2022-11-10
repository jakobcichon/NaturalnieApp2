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

    public record DummyProductModel: ValidatableBindableRecordBase
    {
        private string name;
        private double price;
        private int tax;
        private List<int> taxValuesProvider;
        private TestEnum options;
        private bool inCashRegister;

        [RegexStringValidatorCustom(@"\d")]
        [StringLengthCustom(5)]

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [RangeCustom(2, 5.13)]
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        [HasAdmissibleList("TaxValuesProvider")]
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

        public TestEnum Options
        {
            get { return options; }
            set { SetProperty(ref options, value); }
        }

        public bool InCashRegister
        {
            get { return inCashRegister; }
            set { SetProperty(ref inCashRegister, value); }
        }

    }
}
