namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Common.Extension_Methods;
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;

    public enum ConditionsForNumericalType
    {
        EqualTo,
        GreaterThan,
        LessThan,
    }

    public enum ConditionsForStringType
    {
        Contains,
        Equals,
    }

    public enum ConditionsForSelectionType
    {
        Equals
    }

    public class FilterControlElementViewModel : BindableBase
    {
        #region Fields
        private Type typeToFilter;
        private bool valueVisibility;
        private bool isFilterComplited;
        private List<string> conditions;
        private bool conditionVisibility;
        private string selectedCondition;
        private bool applyButtonVisibility;
        private object selectedValueToFilter;
        private List<string> elementsToFilter;
        private string selectedElementToFilter;
        #endregion

        #region Events
        public EventHandler FilterAplliedHandler;
        #endregion

        #region Constructor
        public FilterControlElementViewModel()
        {
            ElementToFilterVisibility = true;
            ConditionVisibility = false;
            ValueVisibility = false;
            ApplyButtonVisibility = false;

            FilterAppliedCommand = new CommandBase(OnFilterAppliedCommand);
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

        public object SelectedValueToFilter
        {
            get { return selectedValueToFilter; }
            set
            {
                selectedValueToFilter = value;
            }
        }
        #endregion

        #region Provate Methods
        private void OnFilterAppliedCommand(object? obj)
        {
            FilterAplliedHandler?.Invoke(this, EventArgs.Empty);
            IsFilterComplited = true;

        }

        private void OnSelectedElementToFilterChange()
        {
            Type? propType = GetTypeOfSelectedElementToFilter();
            if (propType is null)
            {
                return;
            }

            if(propType.IsNumeric())
            {
                Conditions = Enum.GetNames(typeof(ConditionsForNumericalType)).ToList();
                return;
            }
            if (propType.IsString())
            {
                Conditions = Enum.GetNames(typeof(ConditionsForStringType)).ToList();
                return;
            }
            if (propType.IsBool() || propType.IsEnum())
            {
                Conditions = Enum.GetNames(typeof(ConditionsForSelectionType)).ToList();
                return;
            }

            Conditions = new();
        }

        private void OnModelChange()
        {
            if(TypeToFilter != null) 
            {
                ElementsToFilter = ModelAttributeHelpers.GetPropertiesDisplayableNames(TypeToFilter);
            }
        }

        private Type? GetTypeOfSelectedElementToFilter()
        {
            if(TypeToFilter != null)
            {
                PropertyInfo? prop = ModelAttributeHelpers.GetPropertyByDisplayableName(TypeToFilter, SelectedElementToFilter);

                if (prop == null)
                {
                    prop = TypeToFilter.GetProperty(SelectedElementToFilter);
                }

                if(prop != null)
                {
                    return prop.PropertyType;
                }
            }

            return null;
        }
        #endregion



    }
}
