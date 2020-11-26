namespace CongVanManager
{
    using CongVanManager.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;

    public partial class Setting : ObservableObject
    {
        public string SubscribedWatchlist { get; set; }
    
        public User TruongPhong { get; set; }
        public DateTime LastUpdated { get; set; }

        // Load after USER
        public static void ReloadDatabase()
        {
            _ins.PropertyChanged -= OnSettingChanged;

            var db = DataProvider.Ins.DB;

            var lu = db.QuyDinh.Find("LastUpdated");
            if (lu != null)
                _ins.LastUpdated = DateTime.Parse(lu.GiaTri);
            string headName = db.QuyDinh.Find("TruongPhong")?.GiaTri;
            if (headName != null)
                _ins.TruongPhong = User.DB.FirstOrDefault(item => item.Username == headName);

            // TODO: add subscribed watchlist
            /////////////////////////////////

            _ins.PropertyChanged += OnSettingChanged;
        }

        private static Setting _ins;
        public static Setting Ins {
            get {
                if (_ins == null)
                {
                    _ins = new Setting();
                    ReloadDatabase();
                }
                return _ins;
            }
            private set { }
        }

        static void OnSettingChanged(object obj, PropertyChangedEventArgs args)
        {
            string fieldName = args.PropertyName;
            if (fieldName == "TruongPhong")
            {
                var tp = DataProvider.Ins.DB.QuyDinh.Find("TruongPhong");
                if (tp == null)
                    tp = new View.QuyDinh { MaQuyDinh = "TruongPhong" };
                tp.GiaTri = _ins.TruongPhong.Username;
            }
            if (fieldName == "LastUpdated")
            {
                var lu = DataProvider.Ins.DB.QuyDinh.Find("LastUpdated");
                if (lu == null)
                    lu = new View.QuyDinh { MaQuyDinh = "LastUpdated" };
                lu.GiaTri = _ins.LastUpdated.ToString();
            }
        }
    }
}
