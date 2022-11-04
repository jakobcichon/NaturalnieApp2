namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Properties
        private IDialogBox? dialogBox;

        public IDialogBox? DialogBox
        {
            get { return dialogBox; }
            set
            {
                dialogBox = value;
                OnPropertyChanged();
            }
        }

        public virtual string ScreenInfo => "Ekran menu";

        public virtual bool ShowScreenInfo => true;
        #endregion

        #region Public methods
        public void Load()
        {
            _ = LoadOperation();
        }

        public async Task LoadAsync()
        {
            await LoadOperation();
        }
        #endregion

        #region Private methods
        protected virtual async Task LoadOperation()
        {
            await Task.CompletedTask;
        }
        #endregion

        #region Protected methods
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
