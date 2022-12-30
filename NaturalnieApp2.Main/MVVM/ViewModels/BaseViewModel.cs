namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.Common.Disposable;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseViewModel : DisposableBase, INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        public EventHandler CloseRequestHandler { get; set; } = delegate { }!;
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

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    DialogBox?.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.
                _disposedValue = true;
            }

            // Call the base class implementation.
            base.Dispose(disposing);
        }
        #endregion
        #endregion

        #region Protected methods
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnCloseRequest()
        {
            DialogBox?.Show("Czy na pewno chcesz zamknąć?").AddAction(DialogBoxResults.OK, () =>
            {
                CloseRequestHandler?.Invoke(this, EventArgs.Empty);
            });
        }
        #endregion


    }
}
