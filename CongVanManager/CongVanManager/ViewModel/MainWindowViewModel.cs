using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CongVanManager.Command;
using CongVanManager.View;

public enum PageName { InboxLayout, NewDocLayout_Chooser, NewDocLayout_In, NewDocLayout_Out, SettingLayout, OutboxLayout};

namespace CongVanManager.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly Page[] page = {    new BoxLayout(DocType.In), //0
                                            new NewDocLayout_Chooser(), //1
                                            new NewDocLayout(DocType.In), //2
                                            new NewDocLayout(DocType.Out), //3
                                            new SettingLayout(), //4
                                            new BoxLayout(DocType.Out) //5
        };

        private Page _selectedPage;
        public Page SelectedPage
        {
            get {
                return _selectedPage;
            }
            set {
                _selectedPage = value; OnPropertyChanged();
            }
        }

        private MainWindowViewModel()
        {
            ChangePage(0);
        }

        private static MainWindowViewModel _instance = null;
        public static MainWindowViewModel instance
        {
            get {
                if (_instance == null)
                    _instance = new MainWindowViewModel();
                return _instance;
            }
            private set { }
        }

        private int currentPageIndex = -1;
        void ChangePage(int pageNumber)
        {
            if (pageNumber == currentPageIndex) return;
            SelectedPage = page[pageNumber];
            currentPageIndex = pageNumber;
        }

        public void PageSwitch(PageName messageID)
        {
            switch (messageID)
            {
                case PageName.InboxLayout:
                    ChangePage(0);
                    break;
                case PageName.NewDocLayout_Chooser:
                    ChangePage(1);
                    break;
                case PageName.NewDocLayout_In:
                    ChangePage(2);
                    break;
                case PageName.NewDocLayout_Out:
                    ChangePage(3);
                    break;
                case PageName.SettingLayout:
                    ChangePage(4);
                    break;
                case PageName.OutboxLayout:
                    ChangePage(5);
                    break;
                default:
                    break;
            }
        }

        //COMMANDS
        public ICommand Open_NewDocLayout_Chooser
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       NewDocLayout_ChooserViewModel.instance.SetPreviousPage(currentPageIndex);
                       MainWindowViewModel.instance.PageSwitch(PageName.NewDocLayout_Chooser);
                   });
            }
        }

        public ICommand Open_InboxLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.instance.PageSwitch(PageName.InboxLayout);
                   });
            }
        }

        public ICommand Open_OutboxLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.instance.PageSwitch(PageName.OutboxLayout);
                   });
            }
        }

        public ICommand Open_SettingLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.instance.PageSwitch(PageName.SettingLayout);
                   });
            }
        }
    }
}
