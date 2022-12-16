namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl
{
    using NaturalnieApp2.Common.Extension_Methods;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public class FilterDataSet<T>
    {

        private List<T>? filteredElements;

        public FilterControlElementViewModel FilterInstance { get; set; } = null;
        public FilteredEntity FilterConditionEntity { get; set; } = null;

        public int FilteredElementsCount
        {
            get
            {
                if (FilteredElements is null)
                {
                    return default;
                }

                return FilteredElements.Count;
            }
        }

        public List<T>? FilteredElements
        {
            get { return filteredElements; }
            set
            {
                if (value == null)
                {
                    filteredElements = new List<T>();
                    return;
                }
                filteredElements = value;
            }
        }
    }

    public class FilterControlViewModel<T>
    {
        #region Fields
        private readonly Type typeToFilter;
        private readonly List<FilterDataSet<T>> filtersData; 
        #endregion

        #region Events
        public EventHandler<List<T>> FilterChangedHandler { get; set; } = delegate { }; 
        #endregion

        public FilterControlViewModel(Type typeToFilter)
        {
            FilterConditions = new();
            FilterConditions.CollectionChanged += FilterConditions_CollectionChanged;

            AddButtonCommand = new(OnAddButtonClick, CanExecute);
            this.typeToFilter = typeToFilter;
            filtersData = new();
        }

        #region Properties
        public ObservableCollection<FilterControlElementViewModel> FilterConditions { get; set; }
        public CommandBase AddButtonCommand { get; set; }
        public object Model { get; init; }
        public List<T> ReferenceList { get; set; }
        public List<T> FilteredList { get; private set; }
        public bool IsFilter { get; set; }
        #endregion

        #region Public methods
        public bool CanExecute(object? data)
        {
            return FilterConditions.All(e => e.IsFilterComplited);
        }

        public void OnAddButtonClick(object? data)
        {
            FilterControlElementViewModel filter = new()
            {
                TypeToFilter = typeToFilter,
            };
            filter.FilterRemoveRequestHandler += OnFilterRemoveRequest;
            filter.FilterAplliedHandler += OnFilterApplied;

            FilterConditions.Add(filter);

        }
        #endregion

        #region Protected/Private methods
        private void OnFilterRemoveRequest(object? sender, EventArgs e)
        {
            FilterControlElementViewModel? localSender = sender as FilterControlElementViewModel;
            if (localSender != null)
            {
                localSender.FilterRemoveRequestHandler -= OnFilterRemoveRequest;
                localSender.FilterAplliedHandler -= OnFilterApplied;
                FilterConditions.Remove(localSender);
            }

            UdpateResultList();

            FilterChangedHandler?.Invoke(this, FilteredList);
        }

        private void FilterConditions_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ActionForAddingFilter(e);
                    return;
                case NotifyCollectionChangedAction.Remove:
                    ActionForDeletingFilter(e);
                    return;
            }
        }

        private void ActionForDeletingFilter(NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems == null)
            {
                return;
            }

            foreach (object? item in e.OldItems)
            {
                if (item == null)
                {
                    continue;
                }

                FilterControlElementViewModel? casted = item as FilterControlElementViewModel;

                if (casted == null)
                {
                    continue;
                }

                FilterDataSet<T>? element = filtersData.Find(e => e.FilterInstance == casted);
                if (element != null)
                {
                    filtersData.Remove(element);
                }
            }
        }

        private void ActionForAddingFilter(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
            {
                return;
            }

            foreach (object? item in e.NewItems)
            {
                if (item == null)
                {
                    continue;
                }

                FilterControlElementViewModel? casted = item as FilterControlElementViewModel;

                if (casted == null)
                {
                    continue;
                }

                filtersData.Add(new() { FilterInstance = casted });
            }
        }

        private void OnFilterApplied(object? sender, FilteredEntity e)
        {
            FilterControlElementViewModel? casted = sender as FilterControlElementViewModel;

            if (casted == null)
            {
                return;
            }

            var filterData = filtersData.Find(e => e.FilterInstance.Equals(casted));

            if (filterData == null)
            {
                return;
            }

            filterData.FilterConditionEntity = e;
            filterData.FilteredElements = ReferenceList?.Where(o => FilterAplliedOnListAction(o, e.PropertyName, e.ExpectedValue, e.ComparisonType)).ToList();

            UdpateResultList();

            FilterChangedHandler?.Invoke(this, FilteredList);
        }

        private void UdpateResultList()
        {
            // No filter data at all
            if (filtersData == null)
            {

                FilteredList = ReferenceList;
                return;
            }

            if (filtersData.Count == 0)
            {
                FilteredList = ReferenceList;
                return;
            }

            List<T> resultList = filtersData.First().FilteredElements;

            foreach (FilterDataSet<T> filter in filtersData)
            {
                if (filter is null || filter.FilteredElements is null)
                {
                    continue;
                }

                resultList = resultList.Intersect(filter.FilteredElements).ToList();
            }

            FilteredList = resultList;
            return;
        }

        private bool FilterAplliedOnListAction(T obj, string searchedPropName, object expectedVal, AvailableConditions compType)
        {
            Type objectType = obj?.GetType();

            if (objectType is null)
            {
                return false;
            }

            System.Reflection.PropertyInfo? property = objectType.GetProperty(searchedPropName);

            if (property is null)
            {
                property = objectType.GetPropertyByDisplayableName(searchedPropName);
            }

            if (property is null)
            {
                return false;
            }

            var result = CompareValues(property.GetValue(obj), expectedVal, compType);
            return result;

        }

        private bool CompareValues(object baseValue, object value, AvailableConditions comparisonType)
        {
            IComparable? comparable = (baseValue as IComparable);
            if (comparable == null)
            {
                return false;
            }

            switch (comparisonType)
            {
                case AvailableConditions.Contains:
                    string? baseAsString = (baseValue as string);
                    string? valueAsString = (value as string);
                    if (baseAsString == null || valueAsString == null)
                    {
                        return false;
                    }

                    return baseAsString.Contains(valueAsString, StringComparison.InvariantCultureIgnoreCase);
                case AvailableConditions.EqualTo:
                    return comparable.CompareTo(value) == 0;

                case AvailableConditions.GreaterThan:
                    return comparable.CompareTo(value) > 0;

                case AvailableConditions.LessThan:
                    return comparable.CompareTo(value) < 0;

                default:
                    return false;
            }
        } 
        #endregion

    }
}
