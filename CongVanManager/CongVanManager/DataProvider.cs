using CongVanManager.View;
using CongVanManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager
{
    class DataProvider
    {
        private DataProvider() { }
        private static DataProvider _ins;
        private static Task _uploadRefresher;
        private static Task _downloadRefresher;
        public static DataProvider Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new DataProvider();
                    _ins.DB.Configuration.AutoDetectChangesEnabled = false;
                    _uploadRefresher = _ins.RefreshUploadAsync();
                    _downloadRefresher = _ins.RefreshDownloadAsync();
                }

                return _ins;
            }
        }

        public MyDBContext DB { get; set; } = new MyDBContext();
        
        private const int TimeToRefresh_Upload = 100000; // 10 sec
        private const int TimeToRefresh_Download = 600000; // 10 min

        private async Task RefreshUploadAsync()
        {
            while (true)
            {
                try
                {
                    await Task.Delay(TimeToRefresh_Upload);
                    DB.ChangeTracker.DetectChanges();
                    if (DB.ChangeTracker.HasChanges())
                    {
                        var setting = DB.QuyDinh.Find("LastUpdated");
                        // TODO: remove this
                        //*
                        if (setting == null) {
                            setting = new QuyDinh { MaQuyDinh = "LastUpdated" };
                            DB.QuyDinh.Add(setting);
                        }
                        //*/
                        setting.GiaTri = DateTime.Now.ToString();
                    }
                    DB.SaveChanges();
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
        }

        private async Task RefreshDownloadAsync()
        {
            while (true)
            {
                await Task.Delay(TimeToRefresh_Download);
                try
                {
                    //*
                    DB.Dispose();
                    DB = new MyDBContext();
                    DB.Configuration.AutoDetectChangesEnabled = false;
                    //*/

                    DateTime lastUpdate = Setting.Ins.LastUpdated;

                    User.ReloadDatabase();
                    Setting.ReloadDatabase();

                    if (lastUpdate.CompareTo(Setting.Ins.LastUpdated) >= 0)
                        return;


                    LoaiCongVan.ReloadDatabase();
                    LienHe.ReloadDatabase();
                    KyHieu.ReloadDatabase();
                    CongVan.ReloadDatabase();

                    NguoiDung user = DB.NguoiDung.Find(MainWindowViewModel.Ins.User.Username);
                    if (user != null)
                        user.LastSeen = DateTime.Now;
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
        }
    }
}
