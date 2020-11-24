namespace CongVanManager
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public partial class Setting
    {
        public string SubscribedWatchlist { get; set; }
    
        public User TruongPhong { get; set; }
        public DateTime LastUpdated { get; set; }

        public static void ReloadDatabase()
        {
            var db = DataProvider.Ins.DB;

            _ins.LastUpdated = DateTime.Parse(db.QuyDinh.Find("LastUpdated").GiaTri);
            string headName = db.QuyDinh.Find("TruongPhong")?.GiaTri;
            if (headName != null)
                _ins.TruongPhong = User.DB.FirstOrDefault(item => item.Username == headName);

            // TODO: add subscribed watchlist
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
    }
}
