namespace NaturalnieApp2.Common.Properties
{
    using NaturalnieApp2.Common.Disposable;
    using NaturalnieApp2.Common.Extension_Methods;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Documents;
    using System.Windows.Input;

    public class ProxyPropertyService: DisposableBase, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Fieldsc
        private const string defaultFormat = "{0}";
        private object? propertyContext;
        private string? propertyName;
        private PropertyInfo? propertyInfo;
        private INotifyDataErrorInfo validatableContext;
        #endregion

        public ProxyPropertyService(object propertyContext, string propertyName)
        {
            PropertyContext = propertyContext;
            PropertyName = propertyName;
        }

        #region Properties
        public bool HasErrors
        {
            get
            {
                return validatableContext?.HasErrors ?? false;
            }
        }
        public object? PropertyValue
        {
            get { return GetValue(); }
            set { SetValue(value); }
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

        public string? PropertyDisplayableName
        {
            get { return propertyInfo?.GetDisplayableNameOrName(); }
        }

        public Type? PropertyType
        {
            get
            {
                return propertyInfo?.PropertyType;
            }
        }

        public object? LastValidValue { get; set; }
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

        public string? GetValue()
        {
            if (PropertyName == null)
            {
                return default;
            }

            string? propertyValueAsString = propertyInfo?.GetValue(PropertyContext)?.ToString();
            string format = GetDisplayFormatString();
            string? propertyValueAsStringFormated = string.Format(format, propertyValueAsString);
            return propertyValueAsStringFormated;
        }

        public void SetValue(object? value)
        {
            if (PropertyName == null || propertyInfo == null)
            {
                return;
            }

            object? localObject = ConvertValue(value);

            propertyInfo?.SetValue(PropertyContext, localObject);

            if(!this.HasErrors)
            {
                LastValidValue = localObject;
            }
        }
        #endregion

        #region Private methods
        private object? ConvertValue(object? value)
        {
            if(propertyInfo == null || value == null)
            {
                return null;
            }

            object? localObject = value;

            if (propertyInfo.PropertyType.IsSubclassOf(typeof(Enum)))
            {
                string? stringLocalValue = localObject?.ToString();

                if(stringLocalValue != null)
                {

                    return Enum.Parse(propertyInfo.PropertyType, stringLocalValue);
                }

            }

            if (value is IConvertible && propertyInfo != null)
            {
                try
                {
                    localObject = (value as IConvertible)!.ToType(propertyInfo.PropertyType, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    localObject = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;
                }

                return localObject;

            }

            return localObject;
        }

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

        private string GetDisplayFormatString()
        {
            DisplayFormatAttribute? attribute = propertyInfo?.GetCustomAttribute<DisplayFormatAttribute>();

            if (attribute != null)
            {
                return attribute.DataFormatString ?? defaultFormat;
            }

            return defaultFormat;
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
