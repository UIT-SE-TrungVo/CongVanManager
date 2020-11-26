using CongVanManager.ViewModel;
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

namespace CongVanManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.instance;
        }

        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            switch (pnlSideBar.Visibility)
            {
                case (Visibility.Collapsed): //SHOW sidebar
                    {
                        pnlMainDisplay.SetValue(Grid.ColumnProperty, 1);
                        pnlMainDisplay.SetValue(Grid.ColumnSpanProperty, 1);
                        pnlSideBar.Visibility = Visibility.Visible;
                        break;
                    }
                case (Visibility.Visible): //HIDE sidebar
                    {
                        pnlMainDisplay.SetValue(Grid.ColumnProperty, 0);
                        pnlMainDisplay.SetValue(Grid.ColumnSpanProperty, 2);
                        pnlSideBar.Visibility = Visibility.Collapsed;
                        break;
                    }
            }
        }
    }
}
