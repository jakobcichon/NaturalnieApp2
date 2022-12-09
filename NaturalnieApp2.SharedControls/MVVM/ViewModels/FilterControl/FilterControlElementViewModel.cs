namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Common.Extension_Methods;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public enum AvailableConditions
    {
        EqualTo,
        GreaterThan,
        LessThan,
        Contains
    }

    public class FilteredEntity
    {
        public string PropertyName { get; set; }
        public object ExpectedValue { get; set; }
        public AvailableConditions ComparisonType { get; set; }
    }

    public class FilterControlElementViewModel : ValidatableBindableBase
    {

        private enum ConditionsForNumericalType
        {
            [DisplayableName("równy")]
            EqualTo,

            [DisplayableName("większy niż")]
            GreaterThan,

            [DisplayableName("mniejszy niż")]
            LessThan,
        }

        private enum ConditionsForStringType
        {
            [DisplayableName("zawiera")]
            Contains,

            [DisplayableName("równy")]
            EqualTo,
        }

        private enum ConditionsForSelectionType
        {
            [DisplayableName("równy")]
            EqualTo
        }

        #region Fields
        private Type typeToFilter;
        private bool valueVisibility;
        private bool isFilterComplited;
        private List<string> conditions;
        private bool conditionVisibility;
        private string selectedCondition;
        private bool applyButtonVisibility;
        private dynamic selectedValueToFilter;
        private List<string> elementsToFilter;
        private string selectedElementToFilter;
        private Type? conditionType;
        private Type? selectedElementTofilterType;
        #endregion

        #region Events
        public EventHandler<FilteredEntity> FilterAplliedHandler { get; set; } = delegate { };
        public EventHandler FilterRemoveRequestHandler { get; set; } = delegate { };
        #endregion

        #region Constructor
        public FilterControlElementViewModel()
        {
            ElementToFilterVisibility = true;
            ConditionVisibility = false;
            ValueVisibility = false;
            ApplyButtonVisibility = false;

            FilterAppliedCommand = new CommandBase(OnFilterAppliedCommand);
            RemoveFilterCommand = new CommandBase(OnRemovFilterCommand);
        }
        #endregion

        #region Properties
        public bool IsFilterComplited
        {
            get { return isFilterComplited; }
            set { SetProperty(ref isFilterComplited, value); }
        }

        public List<string> Conditions
        {
            get { return conditions; }
            set
            {
                if (value != null && value.First() != string.Empty)
                {
                    value.Insert(0, string.Empty);
                }
                SetProperty(ref conditions, value);

            }
        }

        public bool ElementToFilterVisibility { get; set; }

        public CommandBase FilterAppliedCommand { get; init; }

        public CommandBase RemoveFilterCommand { get; init; }

        public Type TypeToFilter
        {
            get { return typeToFilter; }
            init
            {
                typeToFilter = value;
                OnModelChange();
            }

        }

        public bool ConditionVisibility
        {
            get { return conditionVisibility; }
            set
            {
                SetProperty(ref conditionVisibility, value);
                if (value == false)
                {
                    ValueVisibility = false;
                }
            }
        }

        public bool ValueVisibility
        {
            get { return valueVisibility; }
            set { SetProperty(ref valueVisibility, value); }
        }

        public bool ApplyButtonVisibility
        {
            get { return applyButtonVisibility; }
            set { SetProperty(ref applyButtonVisibility, value); }
        }

        public List<string> ElementsToFilter
        {
            get { return elementsToFilter; }
            private set
            {
                if (value != null && value.First() != string.Empty)
                {
                    value.Insert(0, string.Empty);
                }
                SetProperty(ref elementsToFilter, value);
            }
        }

        public string SelectedElementToFilter
        {
            get { return selectedElementToFilter; }
            set
            {
                selectedElementToFilter = value;
                OnSelectedElementToFilterChange();
                if (value == null || value == string.Empty)
                {
                    ConditionVisibility = false;
                    return;
                }

                ConditionVisibility = true;
            }
        }

        public string SelectedCondition
        {
            get { return selectedCondition; }
            set
            {
                selectedCondition = value;
                if (value == null || value == string.Empty)
                {
                    ValueVisibility = false;
                    return;
                }

                ValueVisibility = true;
            }
        }

        public dynamic SelectedValueToFilter
        {
            get { return selectedValueToFilter; }
            set
            {
                if(value == null)
                {
                    value = string.Empty;
                }

                dynamic converted = Convert.ChangeType(value, selectedElementTofilterType);

                selectedValueToFilter = converted;
            }
        }
        #endregion

        #region Provate Methods
        private void OnFilterAppliedCommand(object? obj)
        {
            FilterAplliedHandler?.Invoke(this, new FilteredEntity
            {
                PropertyName = SelectedElementToFilter,
                ExpectedValue = SelectedValueToFilter,
                ComparisonType = (AvailableConditions)Enum.Parse(typeof(AvailableConditions), conditionType.GetFieldByDisplayableName(SelectedCondition).Name)
        });
            IsFilterComplited = true;
        }

        private void OnRemovFilterCommand(object? obj)
        {
            FilterRemoveRequestHandler?.Invoke(this, EventArgs.Empty);
        }

        private void OnSelectedElementToFilterChange()
        {
            Type? propType = GetTypeOfSelectedElementToFilter();
            if (propType is null)
            {
                return;
            }

            if (propType.IsGenericType)
            {
                propType = Nullable.GetUnderlyingType(propType);
            }

            selectedElementTofilterType = propType;

            if (propType.IsNumeric())
            {
                Conditions = Enum.GetValues(typeof(ConditionsForNumericalType)).Cast<ConditionsForNumericalType>().First().GetDisplayableNamesOrDefault();
                conditionType = typeof(ConditionsForNumericalType);
                return;
            }
            if (propType.IsString())
            {
                Conditions = Enum.GetValues(typeof(ConditionsForStringType)).Cast<ConditionsForStringType>().First().GetDisplayableNamesOrDefault();
                conditionType = typeof(ConditionsForStringType);
                return;
            }
            if (propType.IsBool() || propType.IsEnum())
            {
                Conditions = Enum.GetValues(typeof(ConditionsForSelectionType)).Cast<ConditionsForSelectionType>().First().GetDisplayableNamesOrDefault();
                conditionType = typeof(ConditionsForSelectionType);
                return;
            }

            Conditions = new();
            conditionType = null;
        }

        private void OnModelChange()
        {
            if (TypeToFilter != null)
            {
                ElementsToFilter = TypeToFilter.GetPropertiesDisplayableNames();
            }
        }

        private Type? GetTypeOfSelectedElementToFilter()
        {
            if (TypeToFilter != null)
            {
                PropertyInfo? prop = TypeToFilter.GetPropertyByDisplayableName(SelectedElementToFilter);

                if (prop == null)
                {
                    prop = TypeToFilter.GetProperty(SelectedElementToFilter);
                }

                if (prop != null)
                {
                    return prop.PropertyType;
                }
            }

            return null;
        }
        #endregion



    }
}
