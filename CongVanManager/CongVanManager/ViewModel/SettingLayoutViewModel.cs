using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager.ViewModel
{
    class SettingLayoutViewModel
    {
        private static SettingLayoutViewModel _instance;
        public static SettingLayoutViewModel instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingLayoutViewModel();
                return _instance;
            }
            private set { }
        }
    }
}
