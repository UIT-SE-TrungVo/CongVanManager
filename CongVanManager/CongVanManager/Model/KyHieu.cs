using CongVanManager.View;
using CongVanManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager
{
    class KyHieu : ObservableObject
    {
        string _maKyHieu;

        public string MaKyHieu { get => _maKyHieu; set => _maKyHieu = value; }

        private static DelayedObservableCollection<KyHieu> _db;

        public KyHieu(View.KyHieuCongVan item)
        {
            MaKyHieu = item.MaKyHieu;
        }

        public KyHieu()
        {
        }

        public static DelayedObservableCollection<KyHieu> DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new DelayedObservableCollection<KyHieu>();
                    ReloadDatabase();
                }
                return _db;
            }
            private set { }
        }
        
        public View.KyHieuCongVan ToKyHieuCongVan()
        {
            return new View.KyHieuCongVan { MaKyHieu = MaKyHieu };
        }

        static void KyHieuCongVanDBChanged(object sender, NotifyCollectionChangedEventArgs arg)
        {
            if (arg.Action == NotifyCollectionChangedAction.Move)
                return;
            if (arg.OldItems != null)
                foreach (KyHieu item in arg.OldItems)
                {
                    var cvs = DataProvider.Ins.DB.LienHe;
                    cvs.Remove(cvs.Find(item.MaKyHieu));
                }
            if (arg.NewItems != null)
                foreach (KyHieu item in arg.NewItems)
                    DataProvider.Ins.DB.KyHieuCongVan.Add(item.ToKyHieuCongVan());
        }

        public static void ReloadDatabase()
        {
            _db.CollectionChanged -= KyHieuCongVanDBChanged;
            
            foreach (View.KyHieuCongVan item in DataProvider.Ins.DB.KyHieuCongVan)
                _db.Add(new KyHieu(item));

            _db.CollectionChanged += KyHieuCongVanDBChanged;
        }
    }
}
