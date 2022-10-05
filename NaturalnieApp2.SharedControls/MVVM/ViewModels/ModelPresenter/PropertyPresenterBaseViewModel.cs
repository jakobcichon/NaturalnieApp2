namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using System.Windows.Data;

    public class PropertyPresenterBaseViewModel : BaseViewModel, IPropertyPresenter
    {
        #region Fields
        private string? displayText;
        private object? displayValue;
        #endregion

        #region Properties

        public string? DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
                OnPropertyChanged();
            }
        }

        public virtual object? DisplayValue
        {
            get { return displayValue; }
            set
            {
                displayValue = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
