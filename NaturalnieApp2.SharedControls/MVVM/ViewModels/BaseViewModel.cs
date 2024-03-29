﻿namespace NaturalnieApp2.SharedControls.MVVM.ViewModels
{
    using NaturalnieApp2.Common.Disposable;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
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
        #endregion

        #region Public methods
        public virtual Task LoadAsync() => Task.CompletedTask;
        #endregion

        #region Protected methods
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
