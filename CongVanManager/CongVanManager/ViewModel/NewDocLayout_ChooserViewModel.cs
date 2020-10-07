using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CongVanManager.Command;

namespace CongVanManager.ViewModel
{
    class NewDocLayout_ChooserViewModel
    {
        private static NewDocLayout_ChooserViewModel _instance = null;
        public static NewDocLayout_ChooserViewModel instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NewDocLayout_ChooserViewModel();
                return _instance;
            }
            private set { }
        }

        public ICommand Open_NewDocLayout_In
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.instance.PageSwitch(PageName.NewDocLayout_In);
                   });
            }
        }

        public ICommand Open_NewDocLayout_Out
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.instance.PageSwitch(PageName.NewDocLayout_Out);
                   });
            }
        }

        public ICommand Open_PreviousPage
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.instance.PageSwitch((PageName)previousPage); //cast <int> to <enum>
                   });
            }
        }

        int previousPage;
        public void SetPreviousPage(int page) { previousPage = page; }
    }
}