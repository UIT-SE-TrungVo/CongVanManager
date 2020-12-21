using CongVanManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace CongVanManager.View
{
    /// <summary>
    /// Interaction logic for ReportPrintPreview.xaml
    /// </summary>
    public partial class ReportPrintPreview : Window
    {
        public ReportPrintPreview(ObservableCollection<BaoCao> bc, int year)
        {
            InitializeComponent();
            this.DataContext = new ReportPrintPreviewViewModel(bc, year);
        }
    }
}
