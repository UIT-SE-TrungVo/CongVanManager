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
    public class ReportLayoutViewModel : ObservableObject
    {
        public ReportLayoutViewModel()
        {
            SelectedYear = 0;
            ResetListYear();
        }

        private ObservableCollection<int> _ListYear = new ObservableCollection<int>();

        public ObservableCollection<int> ListYear
        {
            get { return _ListYear; }
            set { _ListYear = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BaoCao> _ListBaoCao = new ObservableCollection<BaoCao>();

        public ObservableCollection<BaoCao> ListBaoCao
        {
            get { return _ListBaoCao; }
            set { _ListBaoCao = value; OnPropertyChanged(); }
        }

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set { _SelectedYear = value; ResetListBaoCao(); OnPropertyChanged(); }
        }

        public ICommand In
        {
            get
            {
                return new RelayCommand(
                    x =>
                    {
                        ReportPrintPreview rp = new ReportPrintPreview(ListBaoCao, SelectedYear);
                        rp.ShowDialog();
                    }
                    );
            }
        }


        #region function
        public void ResetListYear()
        {
            ListYear.Clear();
            for(int i = 2010; i <= (DateTime.Now.Year); i++)
            {
                ListYear.Add(i);
            }
        }

        public void ResetListBaoCao()
        {
            if (SelectedYear == 0)
                return;
            ListBaoCao.Clear();
            for(int i = 1; i<=12; i++)
            {
                ListBaoCao.Add(new BaoCao { month = i, daGui = 0, daNhan = 0 });
            }
            foreach(CongVan cv in CongVan.DB)
            {
                if(cv.NgayXuLi.Year == SelectedYear)
                {
                    if(cv.StatusCode >= CongVan.StatusCodeEnum.ChoDuyet)
                        ListBaoCao.Where(bc => bc.month == cv.NgayXuLi.Month).First().daGui++;
                    else
                        ListBaoCao.Where(bc => bc.month == cv.NgayXuLi.Month).First().daNhan++;
                }
            }
        }
        #endregion
    }

    public class BaoCao
    {
        public int month { get; set; }
        public int daGui { get; set; }
        public int daNhan { get; set; }
    }
}
