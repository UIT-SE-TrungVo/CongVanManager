using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager.ViewModel
{
    class ActionLayoutViewModel
    {
        private static ActionLayoutViewModel _instance;
        public static ActionLayoutViewModel instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActionLayoutViewModel();
                return _instance;
            }
            private set { }
        }
    }
}
