namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class FilterControlViewModel
    {
        public ObservableCollection<FilterControlElementViewModel> FilterConditions { get; set; }
        public CommandBase AddButtonCommand { get; set; }
        public object Model { get; init; }
        public bool IsFilter { get; set; }
        private Type typeToFilter;

        public FilterControlViewModel(Type typeToFilter)
        {
            FilterConditions = new();
            AddButtonCommand = new(OnAddButtonClick, CanExecute);
            this.typeToFilter = typeToFilter;
        }

        public bool CanExecute(object? data)
        {
            return FilterConditions.All(e => e.IsFilterComplited);
        }

        public void OnAddButtonClick(object? data)
        {
            FilterConditions.Add(new FilterControlElementViewModel { TypeToFilter = typeToFilter });
        }
    }
}
