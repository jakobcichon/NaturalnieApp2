namespace NaturalnieApp2.Common.Binding
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System;
    using System.Linq;
    using NaturalnieApp2.Common.Attributes.DisplayableModel;

    public record ValidatableBindableRecordBase : BindableRecordBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string?>> errors = new();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged = delegate { };

        public System.Collections.IEnumerable GetErrors(string? propertyName)
        {
            if(string.IsNullOrEmpty(propertyName))
            {
                return new List<string>();
            }

            if (errors.ContainsKey(propertyName))
            {
                return errors[propertyName];
            }
            
            return new List<string>();
        }

        [DoNotDisplay]
        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        protected override void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string? propertyName = null)
        {
            ValidateProperty(propertyName, val);
            base.SetProperty<T>(ref member, val, propertyName);
        }

        private void ValidateProperty<T>(string? propertyName, T value)
        {
            if(string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            List<ValidationResult> results = new();
            ValidationContext context = new(this);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {
                List<string?> propErrors = results.Select(c => c.ErrorMessage).ToList();
                if(propErrors.Any())
                {
                    errors[propertyName] = propErrors;
                }
            }
            else
            {
                errors.Remove(propertyName);
            }
            ErrorsChanged!(this, new DataErrorsChangedEventArgs(propertyName));
        }

    }
}
