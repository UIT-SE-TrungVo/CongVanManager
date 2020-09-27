using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CongVanManager.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly Page[] page = { new InboxLayout() };
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
        public MainWindowViewModel()
        {
            SelectedPage = page[0];
        }

        public void ChangePage(int pageNumber)
        {
            SelectedPage = page[pageNumber];
        }

        public void PageSwitch(int messageID)
        {
            switch (messageID)
            {
                case 0:
                    ChangePage(0);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
    }
}
