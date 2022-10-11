namespace NaturalnieApp2.Common.Properties
{
    using NaturalnieApp2.Common.Disposable;
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public class ProxyPropertyService: DisposableBase, INotifyPropertyChanged
    {
        #region Fields
        private object? propertyOwnerObject;

        #endregion

        #region Properties
        public object? PropertyOwnerObject
        {
            get { return propertyOwnerObject; }
            init
            {
                propertyOwnerObject = value;
                OnPropertyObjectChange();
            }
        }

        public string PropertyName { get; init; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        #region Public methods
        public object? GetValue()
        {
            if (PropertyName == null)
            {
                return default;
            }

            return PropertyOwnerObject?.GetType().GetProperty(PropertyName)?.GetValue(PropertyOwnerObject);
        }

        public void SetValue(object value)
        {
            if (PropertyName == null)
            {
                return;
            }

            PropertyInfo? propertyInfo = PropertyOwnerObject?.GetType().GetProperty(PropertyName);

            object? localObject = value;
            if (value is IConvertible && propertyInfo != null)
            {
                localObject = (value as IConvertible)!.ToType(propertyInfo.PropertyType, null);
            }
            propertyInfo?.SetValue(PropertyOwnerObject, localObject);
        }
        #endregion

        #region Private methods
        private void OnPropertyObjectChange()
        {
            Type? changeInterface = PropertyOwnerObject?.GetType().GetInterface("INotifyPropertyChanged");

            if (changeInterface != null)
            {
                (PropertyOwnerObject as INotifyPropertyChanged)!.PropertyChanged += ProxyProperty_PropertyChanged;
            }
        }

        private void ProxyProperty_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
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
                    (PropertyOwnerObject as INotifyPropertyChanged)!.PropertyChanged -= ProxyProperty_PropertyChanged;
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
