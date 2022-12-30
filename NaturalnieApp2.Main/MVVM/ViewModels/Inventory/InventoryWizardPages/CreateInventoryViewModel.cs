namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory.InventoryWizardPages
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.Main.MVVM.Models.Inventory;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Printing.IndexedProperties;
    using System.Windows;
    using System.Windows.Documents;

    public class CreateInventoryViewModel : ValidatableBindableBase
    {
        #region Fields
        private InventoryModelDTO model;
        private static List<string> consideredModelsElements = new List<string>() { "InventoryName", "PersonName" };
        #endregion

        #region Events
        public EventHandler<InventoryModelDTO> InvetoryCreatedHandler { get; set; } = delegate { }!;
        public EventHandler PreviousPageRequestHandler = delegate { }!;
        #endregion

        #region Properties
        public List<string> ExisitngInvetoriesNames { get; private set; } = new List<string>();
        public InventoryModelDTO Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        private ObservableCollection<ProxyPropertyService> items = new();

        public ObservableCollection<ProxyPropertyService> Items
        {
            get { return items; }
        }

        public CommandBase CreateCommand { get; set; }
        public CommandBase PreviousPageCommand { get; set; }
        #endregion

        public CreateInventoryViewModel()
        {
            CreateCommand = new(OnCreateRequest, CanCreate);
            PreviousPageCommand = new(OnPreviousPageRequest);
            Model = new InventoryModelDTO();

            Model.GetType().GetProperties().Where(p => consideredModelsElements.Contains(p.Name)).ToList().ForEach(p => Items.Add(new ProxyPropertyService(Model, p.Name)));

            Model.InventoryName = CreateDefaultName();
            Model.PropertyChanged += ModelPropertyChanged;

            Model.ValidateModel();
        }

        private string CreateDefaultName()
        {
            string tempName = $"Inwentaryzacja_{DateTime.Now.Year}";
            if(NameExist(tempName))
            {
               return GenerateName(tempName);
            }

            return tempName;
        }

        private string GenerateName(string name, int suffix=1)
        {
            string tempName =  name + suffix;
            if(NameExist(tempName)) 
            {
                return GenerateName(name, ++suffix);
            }

            return tempName;
        }

        private bool NameExist(string name)
        {
            return ExisitngInvetoriesNames?.Any(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? false;
        }

        private void ModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (consideredModelsElements.Any(p => p.Equals(e.PropertyName)))
            {
                CreateCommand.OnCanExecuteChange();
            }
        }

        private void OnCreateRequest(object? data)
        {
            InvetoryCreatedHandler?.Invoke(this, Model);
        }

        private void OnPreviousPageRequest(object? data)
        {
            PreviousPageRequestHandler?.Invoke(this, EventArgs.Empty);
        }

        private bool CanCreate(object? data) 
        {
            return consideredModelsElements.All(Model.IsPropertyValid);
        }

    }
}
