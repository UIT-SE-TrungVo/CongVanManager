using CongVanManager.View;
using CongVanManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager.Model
{
    class KyHieuCongVan : ObservableObject
    {
        string _maKyHieu;

        public string MaKyHieu { get => _maKyHieu; set => _maKyHieu = value; }

        private static DelayedObservableCollection<KyHieuCongVan> _db;

        public KyHieuCongVan(View.KyHieuCongVan item)
        {
            MaKyHieu = item.MaKyHieu;
        }

        public static DelayedObservableCollection<KyHieuCongVan> DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new DelayedObservableCollection<KyHieuCongVan>();
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
                foreach (KyHieuCongVan item in arg.OldItems)
                {
                    var cvs = DataProvider.Ins.DB.LienHe;
                    cvs.Remove(cvs.Find(item.MaKyHieu));
                }
            if (arg.NewItems != null)
                foreach (KyHieuCongVan item in arg.NewItems)
                    DataProvider.Ins.DB.KyHieu.Add(item.ToKyHieuCongVan());
        }

        private static void ReloadDatabase()
        {
            _db.CollectionChanged -= KyHieuCongVanDBChanged;

            foreach (View.KyHieuCongVan item in DataProvider.Ins.DB.KyHieu)
                _db.Add(new KyHieuCongVan(item));

            _db.CollectionChanged += KyHieuCongVanDBChanged;
        }
    }
}
