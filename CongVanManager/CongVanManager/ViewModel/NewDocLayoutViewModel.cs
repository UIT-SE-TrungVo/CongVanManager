using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

public enum DocType { In, Out };

namespace CongVanManager.ViewModel
{
    public class NewDocLayoutViewModel : ObservableObject
    {
        public DocType NewDocLayout_Type;
        public int iNewDocLayout_Type { get; set; }

        public NewDocLayoutViewModel(DocType type)
        {
            NewDocLayout_Type = type;
            iNewDocLayout_Type = (int)NewDocLayout_Type;

            NgayNhan = DateTime.Today;
            NgayTrenCongVan = DateTime.Today;
            listLoaiCongVan = new ObservableCollection<string>();

            ResetListCongVan();
            ResetListMaKyHieu();
        }
        #region Binding
        //So vao
        private string _SoVao;

        public string SoVao
        {
            get { return _SoVao; }
            set { _SoVao = value; OnPropertyChanged(); }
        }
        //Gio nhan
        private string _GioNhan;

        public string GioNhan
        {
            get { return _GioNhan; }
            set { _GioNhan = value; OnPropertyChanged(); }
        }
        //Ngay nhan
        private DateTime _NgayNhan;

        public DateTime NgayNhan
        {
            get { return _NgayNhan; }
            set { _NgayNhan = value; OnPropertyChanged();}
        }
        //NgayTrenCongVan
        private DateTime _NgayTrenCongVan;

        public DateTime NgayTrenCongVan
        {
            get { return _NgayTrenCongVan; }
            set { _NgayTrenCongVan = value; OnPropertyChanged(); }
        }
        //LoaiCongVan
        private ObservableCollection<string> _listLoaiCongVan;

        public ObservableCollection<string> listLoaiCongVan
        {
            get { return _listLoaiCongVan; }
            set { _listLoaiCongVan = value; OnPropertyChanged();  }
        }

        private string _selectedLoaiCongVan;

        public string selectedLoaiCongVan
        {
            get { return _selectedLoaiCongVan; }
            set {
                _selectedLoaiCongVan = value; OnPropertyChanged();
                if(selectedLoaiCongVan == "(Thêm mới loại công văn)")
                {
                    ShowDialogLCV();
                }
            }
        }

        public void ResetListCongVan()
        {
            if(listLoaiCongVan.Count != 0)
                listLoaiCongVan.Clear();
            foreach(LoaiCongVan item in LoaiCongVan.DB)
            {
                listLoaiCongVan.Add(item.Id);
            }
            listLoaiCongVan.Add("(Thêm mới loại công văn)");
        }
        //So ki hieu
        private string _SoKyHieu;

        public string SoKyHieu
        {
            get { return _SoKyHieu; }
            set { _SoKyHieu = value; OnPropertyChanged(); }
        }
        //chu ki hieu
        private ObservableCollection<string> _listMaKyHieu = new ObservableCollection<string>();

        public ObservableCollection<string> listMaKyHieu
        {
            get { return _listMaKyHieu; }
            set { _listMaKyHieu = value; OnPropertyChanged(); }
        }

        private string _MaKyHieu;

        public string MaKyHieu
        {
            get { return _MaKyHieu; }
            set { _MaKyHieu = value; OnPropertyChanged(); }
        }

        public void ResetListMaKyHieu()
        {
            if (listMaKyHieu.Count != 0)
                listMaKyHieu.Clear();
            //foreach (LoaiCongVan item in LoaiCongVan.DB)
            //{
            //    listMaKyHieu.Add(item.Id);
            //}
            listMaKyHieu.Add("(Thêm mới loại công văn)");
        }
        //Trich yeu
        private string _TrichYeu;

        public string TrichYeu
        {
            get { return _TrichYeu; }
            set { _TrichYeu = value; OnPropertyChanged(); }
        }
        //Noi gui
        private string _NoiGui;

        public string NoiGui
        {
            get { return _NoiGui; }
            set { _NoiGui = value; OnPropertyChanged(); }
        }
        //Noi nhan
        private string _NoiNhan;

        public string NoiNhan
        {
            get { return _NoiNhan; }
            set { _NoiNhan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<LienHeShort> _DSNoiNhan = new ObservableCollection<LienHeShort>();

        public ObservableCollection<LienHeShort> DSNoiNhan
        {
            get
            {
                return _DSNoiNhan; 
            }
            set
            {
                _DSNoiNhan = value;
                OnPropertyChanged();
            }
        }

        //Ghi chu
        private string _GhiChu;

        public string GhiChu
        {
            get { return _GhiChu; }
            set { _GhiChu = value; OnPropertyChanged(); }
        }
        //pdf file name
        private string _filename;

        public string filename
        {
            get { return _filename; }
            set { _filename = value; OnPropertyChanged(); }
        }

        private string _showFileName;

        public string showFileName
        {
            get { return _showFileName; }
            set { _showFileName = value; OnPropertyChanged(); }
        }
        //ThemLoaiCongVan
        private bool _IsDialogLoaiCongVanOpen;

        public bool IsDialogLoaiCongVanOpen
        {
            get { return _IsDialogLoaiCongVanOpen; }
            set { _IsDialogLoaiCongVanOpen = value; OnPropertyChanged(); }
        }
        private string _newLoaiCongVan;

        public string newLoaiCongVan
        {
            get { return _newLoaiCongVan; }
            set { _newLoaiCongVan = value; OnPropertyChanged(); }
        }


        #endregion
        #region Command
        public ICommand PinPdf
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "Pdf Files|*.pdf";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        filename = open.FileName;
                        showFileName = open.SafeFileName;
                    }
                });
            }
        }

        public ICommand ClickOnFileNameCommand
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    if(filename!= null)
                        Process.Start(filename);
                });
            }
        }

        public ICommand ClickThemNoiNhan
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    LienHeShort lh = new LienHeShort();
                    lh.Email = NoiNhan;
                    lh.TenLienHe = NoiNhan;
                    DSNoiNhan.Add(lh);
                    NoiNhan = "";
                });
            }
        }

        public ICommand XoaNoiNhan
        {
            get
            {
                return new RelayCommand<LienHeShort>(
                x =>
                {
                    DSNoiNhan.Remove(x);
                });
            }
        }

        public ICommand SaveLoaiCongVan
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    LoaiCongVan lcv = new LoaiCongVan() { Id = newLoaiCongVan };
                    LoaiCongVan.DB.Add(lcv);
                    ResetListCongVan();
                    IsDialogLoaiCongVanOpen = false;
                    selectedLoaiCongVan = newLoaiCongVan;
                    newLoaiCongVan = "";
                });
            }
        }
        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    newLoaiCongVan = "";
                    IsDialogLoaiCongVanOpen = false;
                    selectedLoaiCongVan = "";
                });
            }
        }
        #endregion
        #region other function
        public void ShowDialogLCV()
        {
            IsDialogLoaiCongVanOpen = true;
        }
        #endregion
    }
    public class LienHeShort
    {
        public string Email { get; set; }
        public string TenLienHe { get; set; }
    }
}
