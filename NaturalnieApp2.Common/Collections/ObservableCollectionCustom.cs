namespace NaturalnieApp2.Common.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ObservableCollectionCustom<T> : ObservableCollection<T> 
    {
        public void AddRange(IEnumerable<T> items)
        {
            this.CheckReentrancy();

            int startIndex = Count;

            foreach (T? item in items)
            {
                this.Items.Add(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>(items), startIndex));
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        }
    }
}
