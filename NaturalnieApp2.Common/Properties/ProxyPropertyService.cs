namespace NaturalnieApp2.Common.Properties
{
    using NaturalnieApp2.Common.Disposable;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Documents;

    public class ProxyPropertyService: DisposableBase, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Fields
        private object? propertyContext;
        private string? propertyName;
        private PropertyInfo? propertyInfo;
        private INotifyDataErrorInfo validatableContext;
        private bool hasErrors;
        #endregion

        #region Properties
        public bool HasErrors
        {
            get
            {
                return validatableContext?.HasErrors ?? false;
            }
        }

        public object? PropertyContext
        {
            get { return propertyContext; }
            init
            {
                propertyContext = value;
                OnPropertyObjectChange();
                UpdatePropertyInfo();
                UpdateValidatableContext();
            }
        }

        public string? PropertyName
        {
            get { return propertyName; }
            init 
            { 
                propertyName = value; 
                UpdatePropertyInfo();
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged = delegate { };
        #endregion

        #region Public methods
        public IEnumerable GetErrors(string? propertyName)
        {
            return validatableContext?.GetErrors(this.propertyName) ?? new List<string>();
        }

        public object? GetValue()
        {
            if (PropertyName == null)
            {
                return default;
            }

            return propertyInfo?.GetValue(PropertyContext);
        }

        public void SetValue(object value)
        {
            if (PropertyName == null || propertyInfo == null)
            {
                return;
            }

            object? localObject = value;

            if (value is IConvertible && propertyInfo != null)
            {
                localObject = (value as IConvertible)!.ToType(propertyInfo.PropertyType, null);
            }
            propertyInfo?.SetValue(PropertyContext, localObject);
        }
        #endregion

        #region Private methods
        private void OnPropertyObjectChange()
        {
            Type? changeInterface = PropertyContext?.GetType().GetInterface("INotifyPropertyChanged");

            if (changeInterface != null)
            {
                (PropertyContext as INotifyPropertyChanged)!.PropertyChanged += ProxyProperty_PropertyChanged;
            }
        }

        private void ProxyProperty_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void UpdatePropertyInfo()
        {
            if(PropertyContext != null && PropertyName != null)
            {
                propertyInfo = PropertyContext.GetType().GetProperty(PropertyName);
            }
        }

        private void UpdateValidatableContext()
        {
            if (PropertyContext as INotifyDataErrorInfo != null)
            {
                validatableContext = (PropertyContext as INotifyDataErrorInfo)!;
                validatableContext.ErrorsChanged += this.ValidatableContext_ErrorsChanged;
            }
        }

        private void ValidatableContext_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(PropertyName));
        }
        #endregion

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    (PropertyContext as INotifyPropertyChanged)!.PropertyChanged -= ProxyProperty_PropertyChanged;
                    if(validatableContext != null)
                    {
                        validatableContext.ErrorsChanged -= this.ValidatableContext_ErrorsChanged;
                    }

                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.
                _disposedValue = true;
            }

            // Call the base class implementation.
            base.Dispose(disposing);
        }
        #endregion
    }
}
