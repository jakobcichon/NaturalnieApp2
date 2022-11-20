namespace NaturalnieApp2.Main.MVVM.Models
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Main.Interfaces.Model;

    internal record ModelBase : ValidatableBindableRecordBase, IModel
    {
        [DoNotDisplay]
        public bool IsValid => !this.HasErrors;
    }
}
