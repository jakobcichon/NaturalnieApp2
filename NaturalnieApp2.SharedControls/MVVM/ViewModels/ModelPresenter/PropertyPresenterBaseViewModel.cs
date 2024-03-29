﻿namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using System.Windows.Data;

    public class PropertyPresenterBaseViewModel : BaseViewModel, IPropertyPresenter
    {
        #region Fields
        private string? headerText;
        private IPropertyPresenterDataField? propertyPresenterDataField;
        #endregion

        #region Properties
        public string? HeaderText
        {
            get { return headerText; }
            set
            {
                headerText = value;
                OnPropertyChanged();
            }
        }

        public IPropertyPresenterDataField? PropertyPresenterDataField
        {
            get { return propertyPresenterDataField; }
            set
            {
                propertyPresenterDataField = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
