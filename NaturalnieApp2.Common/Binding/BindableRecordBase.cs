namespace NaturalnieApp2.Common.Binding
{
    using NaturalnieApp2.Common.Disposable;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public record BindableRecordBase : DisposableRecordBase, INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T member, T val,
                   [CallerMemberName] string? propertyName = null)
        {
            if (object.Equals(member, val))
            {
                return;
            }

            member = val;
            PropertyChanged!(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged!(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
    }
}

