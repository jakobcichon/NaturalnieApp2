namespace NaturalnieApp2.Common.Binding
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System;
    using System.Linq;

    public class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string?>> errors = new();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged = delegate { };

        public System.Collections.IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return new List<string>();
            }

            if (errors.ContainsKey(propertyName))
            {
                return errors[propertyName];
            }

            return new List<string>();
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        protected void SetProperty<T>(ref T member, T val, Type propertyType,
            [CallerMemberName] string? propertyName = null)
        {
            bool isValid = ValidatePropertyType(propertyName, val, propertyType);
            if (isValid)
            {
                SetProperty(ref member, val, propertyName);
            }
        }

        protected override void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string? propertyName = null)
        {
            bool isValid = ValidateProperty(propertyName, val);

            if (!isValid)
            {
                return;
            }

            base.SetProperty(ref member, val, propertyName);
        }

        private bool ValidateProperty<T>(string? propertyName, T value)
        {
            bool retValue = true;
            if (string.IsNullOrEmpty(propertyName))
            {
                return retValue;
            }

            List<ValidationResult> results = new();
            ValidationContext context = new(this);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {
                List<string?> propErrors = results.Select(c => c.ErrorMessage).ToList();
                if (propErrors.Any())
                {
                    errors[propertyName] = propErrors;
                    retValue = false;
                }
            }
            else
            {
                errors.Remove(propertyName);
                retValue = true;
            }
            ErrorsChanged!(this, new DataErrorsChangedEventArgs(propertyName));

            return retValue;
        }

        protected bool ValidatePropertyType<T>(string? propertyName, T value, Type propertyType)
        {
            bool retValue = true;
            if (string.IsNullOrEmpty(propertyName))
            {
                return retValue;
            }

            if (propertyType is null)
            {
                return retValue;
            }

            try
            {
                Convert.ChangeType(value, propertyType);
                errors.Remove(propertyName);
                retValue = true;
            }
            catch (Exception)
            {
                errors[propertyName] = new() { "Błędny typ zmiennej" };
                retValue = false;
            }

            ErrorsChanged!(this, new DataErrorsChangedEventArgs(propertyName));

            return retValue;
        }

    }
}
