using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CongVanManager.ViewModel
{
    public class DelayedObservableCollection<T> : ObservableCollection<T>
    where T : class, INotifyPropertyChanged
    {
        public DelayedObservableCollection()
        : base()
        {
            _checkForPropertyChange = CheckForPropertyChangeAsync();

            CollectionChanged += TrulyObservableCollection_CollectionChanged;
        }

        public DelayedObservableCollection(IEnumerable<T> ts)
            :base(ts)
        {
            _checkForPropertyChange = CheckForPropertyChangeAsync();

            foreach (Object item in this)
            {
                (item as INotifyPropertyChanged).PropertyChanged += item_PropertyChanged;
            }

            CollectionChanged += TrulyObservableCollection_CollectionChanged;
        }

        void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += item_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= item_PropertyChanged;
                }
            }
        }

        HashSet<T> newItem = null;
        const int CheckForPropertyChangeTime = 1000; // 60 sec
        Task _checkForPropertyChange;

        async Task CheckForPropertyChangeAsync()
        {
            while (true)
            {
                CallPropertyChanged();
                await Task.Delay(CheckForPropertyChangeTime);
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (newItem == null)
                newItem = new HashSet<T>();
            newItem.Add(sender as T);
        }
        
        void CallPropertyChanged()
        {
            if (newItem != null)
            {
                foreach (T item in newItem)
                    this.Remove(item);
                foreach (T item in newItem)
                    this.Add(item);

                newItem = null;
            }
        }
    }
}
