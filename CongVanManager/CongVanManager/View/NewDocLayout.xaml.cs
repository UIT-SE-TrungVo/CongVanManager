using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for NewDocLayout.xaml
    /// </summary>
    public partial class NewDocLayout : Page
    {
        public NewDocLayout(DocType type)
        {
            InitializeComponent();
            this.DataContext = new NewDocLayoutViewModel(type);
        }

        public NewDocLayout(CongVanManager.CongVan congVan)
        {
            InitializeComponent();
            this.DataContext = new NewDocLayoutViewModel(true, congVan);
        }

        private void btnConfirmSender_Click(object sender, RoutedEventArgs e)
        {
            if (btnSender.Visibility == Visibility.Visible) return;

            btnSender.Visibility = Visibility.Visible;
            tbSender.Visibility = Visibility.Collapsed;
            btnConfirmSender.Visibility = Visibility.Collapsed;
        }

        private void btnSender_Click(object sender, RoutedEventArgs e)
        {
            if (btnSender.Visibility == Visibility.Collapsed) return;

            btnSender.Visibility = Visibility.Collapsed;
            tbSender.Visibility = Visibility.Visible;
            btnConfirmSender.Visibility = Visibility.Visible;
        }
    }
}
