namespace NaturalnieApp2.Main.MVVM.Models.Product
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Attributes.ValidationRules;
    using NaturalnieApp2.Common.Binding;
    using System.Collections;
    using System.Runtime.CompilerServices;

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
        private IList taxValuesProvider;
        private TestEnum options;

        [RegexStringValidatorCustom(@"\d")]
        [StringLengthCustom(5)]
        [CanBeDisplayed]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [CanBeDisplayed]
        [RangeCustom(2, 5.13)]
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        [CanBeDisplayed]
        [HasAdmissibleList("TaxValuesProvider")]
        public int Tax
        {
            get { return tax; }
            set { SetProperty(ref tax, value); }
        }

        public IList TaxValuesProvider
        {
            get { return taxValuesProvider; }
            set { SetProperty(ref taxValuesProvider, value); }
        }

        [CanBeDisplayed]
        public TestEnum Options
        {
            get { return options; }
            set { SetProperty(ref options, value); }
        }

    }
}
