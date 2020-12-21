using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    class ReportPrintPreviewViewModel : ObservableObject
    {
        public ReportPrintPreviewViewModel(ObservableCollection<BaoCao> bc, int year)
        {
            SelectedYear = year;
            ListBaoCao = bc;
        }

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set { _SelectedYear = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BaoCao> _ListBaoCao = new ObservableCollection<BaoCao>();

        public ObservableCollection<BaoCao> ListBaoCao
        {
            get { return _ListBaoCao; }
            set { _ListBaoCao = value; OnPropertyChanged(); }
        }
    }
}
