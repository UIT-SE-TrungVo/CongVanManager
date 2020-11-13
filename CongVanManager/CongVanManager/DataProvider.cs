using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager
{
    class DataProvider
    {
        private static DataProvider _ins;
        private static Task _refresher;
        public static DataProvider Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new DataProvider();
                    _refresher = _ins.RefreshAsync();
                }

                return _ins;
            }

            set { _ins = value; _refresher = _ins.RefreshAsync(); }
        }

        public CONGVANMANAGEREntities DB { get; set; } = new CONGVANMANAGEREntities();

        private bool Lock;
        private const int TimeToRefresh = 10000;

        private async Task RefreshAsync()
        {
            while (true)
            {
                DB.SaveChanges();
                await Task.Delay(TimeToRefresh);
            }
        }
    }
}
