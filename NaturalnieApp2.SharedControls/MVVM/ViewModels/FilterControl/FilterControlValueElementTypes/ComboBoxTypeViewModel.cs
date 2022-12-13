namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl.FilterControlValueElementTypes
{
    using System.Collections.ObjectModel;

    internal class ComboBoxTypeViewModel : ValueElementTypesBaseViewModel
    {
        public ObservableCollection<object> ItemsList { get; private set; }

        public ComboBoxTypeViewModel(object propertyContext, string propertyName) : base(propertyContext, propertyName)
        {
            ItemsList = new();
        }
    }

}