namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Numerics;
    using System.Threading.Tasks;
    using System.Windows.Documents;

    public class FilterDataSet<T>
    {

        private List<T>? filteredElements;

        public FilterControlElementViewModel FilterInstance { get; set; }
        public FilteredEntity FilterConditionEntity { get; set; }
        public List<T>? FilteredElements
        {
            get { return filteredElements; }
            set 
            { 
                if(value == null)
                {
                    filteredElements= new List<T>();
                    return;
                }
                filteredElements = value; 
            }
        }


    }


    public class FilterControlViewModel<T>
    {
        private Type typeToFilter;

        private List<FilterDataSet<T>> filtersData;

        public EventHandler<List<T>> FilterChangedHandler { get; set; } = delegate { };

        public FilterControlViewModel(Type typeToFilter)
        {
            FilterConditions = new();
            FilterConditions.CollectionChanged += FilterConditions_CollectionChanged;

            AddButtonCommand = new(OnAddButtonClick, CanExecute);
            this.typeToFilter = typeToFilter;
            filtersData = new();
        }

        public ObservableCollection<FilterControlElementViewModel> FilterConditions { get; set; }
        public CommandBase AddButtonCommand { get; set; }
        public object Model { get; init; }
        public List<T> ReferenceList { get; set; }
        public List<T> FilteredList { get; private set; }
        public bool IsFilter { get; set; }

        private void FilterConditions_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ActionForAddingFilter(e);

            ActionForDeletingFilter(e);
        }

        private void ActionForDeletingFilter(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
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

                    FilterDataSet<T>? element = filtersData.Find(e => e.FilterInstance == casted);
                    if(element != null)
                    {
                        filtersData.Remove(element);
                    }
                }
            }
        }

        private void ActionForAddingFilter(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
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
        }

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

        private void OnFilterRemoveRequest(object? sender, EventArgs e)
        {
            FilterControlElementViewModel? localSender = sender as FilterControlElementViewModel;
            if (localSender == null)
            {
                return;
            }

            localSender.FilterRemoveRequestHandler -= OnFilterRemoveRequest;
            localSender.FilterAplliedHandler -= OnFilterApplied;
            FilterConditions.Remove(localSender);

            UdpateResultList();
        }

        private void OnFilterApplied(object? sender, FilteredEntity e)
        {
            FilterControlElementViewModel? casted = sender as FilterControlElementViewModel;

            if (casted == null)
            {
                return;
            }

            var filterData = filtersData.Find(e => e.FilterInstance.Equals(casted));

            if(filterData == null)
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
            List<T> resultList = filtersData?.First().FilteredElements ?? new();
            if(resultList.Count > 0)
            {
                foreach (FilterDataSet<T> filter in filtersData)
                {
                    if (filter.FilteredElements == null)
                    {
                        continue;
                    }

                    resultList = resultList.Intersect(filter.FilteredElements).ToList();
                }
            }

            FilteredList = resultList;
        }

        private bool FilterAplliedOnListAction(T obj, string searchedPropName, object expectedVal, AvailableConditions compType)
        {

            System.Reflection.PropertyInfo? property = obj.GetType().GetProperty(searchedPropName);
            if (property == null)
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

    }
}
