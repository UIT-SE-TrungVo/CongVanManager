using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CongVanManager.ViewModel;

namespace CongVanManager.View
{
    /// <summary>
    /// Interaction logic for SettingLayout.xaml
    /// </summary>
    public partial class SettingLayout : Page
    {
        public SettingLayout()
        {
            InitializeComponent();
            this.DataContext = SettingLayoutViewModel.instance;
        }
    }
}
