using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows;
using System.Windows.Input;
using CongVanManager.Command;
using CongVanManager.View;

namespace CongVanManager.ViewModel
{
    class InboxLayoutViewModel : ObservableObject
    {
        #region DanhSachCongVan
        public ICollection<CongVan> DSCongVan
        {
            get { return CongVan.DB./*Where(Filter).*/ToList(); }
            set
            // Call to refresh data, 
            // Does not set value
            { OnPropertyChanged("AmountOfLetters"); }
        }

        public int AmountOfLetters
        {
            get { return CongVan.DB.Count; }
        }
        #endregion

        #region SelectedCongVan

        private CongVan _selectedCongVan;

        public CongVan SelectedCongVan
        {
            get { return _selectedCongVan; }
            set
            {
                _selectedCongVan = value;
                OnPropertyChanged("SelectedCongVanKeyNumber");
                OnPropertyChanged("SelectedCongVanNumber");
                OnPropertyChanged("SelectedLoaiCongVan");
                OnPropertyChanged("SelectedCongVanSender");
                OnPropertyChanged("SelectedCongVanTrichYeu");
                OnPropertyChanged("SelectedCongVanSentDate");
                OnPropertyChanged("SelectedCongVanNote");
                OnPropertyChanged("SelectedCongVanKeyNumber");
                OnPropertyChanged("SelectedCongVanNumber");
                OnPropertyChanged("SelectedCongVanDestination");
            }
        }
        #endregion

        #region SelectedCongVanDetail
        public string SelectedLoaiCongVan
        {
            get { return _selectedCongVan?.LoaiCongVan?.Id; }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (_selectedCongVan.LoaiCongVan.Id != value.ToUpper())
                {
                    LoaiCongVan correctLoaiCongVan = LoaiCongVan.DB.First(
                        (LoaiCongVan item) => { return item.Id == value.ToUpper(); });
                    _selectedCongVan.LoaiCongVan = correctLoaiCongVan;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public string SelectedCongVanTrichYeu
        {
            get { return _selectedCongVan?.TrichYeu; }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (_selectedCongVan.TrichYeu != value)
                {
                    _selectedCongVan.TrichYeu = value;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public string SelectedCongVanKeyNumber
        {
            get
            {
                return _selectedCongVan?.SoKyHieu;
            }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (_selectedCongVan.SoKyHieu != value)
                {
                    _selectedCongVan.SoKyHieu = value;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public int? SelectedCongVanNumber
        {
            get { return _selectedCongVan?.SoCongVan; }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (SelectedCongVanNumber != value)
                {
                    _selectedCongVan.SoCongVan = value;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public string SelectedCongVanSender
        {
            get { return _selectedCongVan?.NoiGui?.Name; }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (SelectedCongVanSender != value)
                {
                    LienHe correctSender = LienHe.DB.First(
                        (LienHe item) => { return item.Name == value || item.Email == value; });
                    _selectedCongVan.NoiGui = correctSender;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public DateTime SelectedCongVanSentDate
        {
            get
            {
                return (_selectedCongVan?.NgayCongVan).
                  GetValueOrDefault(DateTime.MinValue);
            }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (SelectedCongVanSentDate != value)
                {
                    _selectedCongVan.NgayCongVan = value;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public string SelectedCongVanNote
        {
            get { return _selectedCongVan?.GhiChu; }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (SelectedCongVanNote != value)
                {
                    _selectedCongVan.GhiChu = value;
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        public ICollection<NoiNhan> SelectedCongVanDestination
        {
            get { return _selectedCongVan?.DanhSachNoiNhan; }
            set
            {
                OnPropertyChanged();
                if (_selectedCongVan == null)
                    return;
                if (SelectedCongVanDestination != value)
                {
                    _selectedCongVan.DanhSachNoiNhan = new ObservableCollection<NoiNhan>(value);
                    ValueChanged(_selectedCongVan);
                }
            }
        }
        #endregion
        /*
        #region FilterSetting
        private List<Func<CongVan, bool>> filterList = new List<Func<CongVan, bool>>(4);
        private Func<CongVan, bool> defaultFilter = (item) => true;
        private bool Filter(CongVan item)
        {
            foreach(var func in filterList)
                if (func?.Invoke(item) == false)
                    return false;
            return true;
        }
        private bool? _chuaDoc = true;
        public bool? ChuaDoc
        {
            get => _chuaDoc;
            set
            {
                _chuaDoc = value;
                if (value == null)
                    filterList[0] = defaultFilter;
                else
                    filterList[0] = (item) => 
                        !item.StatusCode.HasFlag(CongVan.StatusCodeEnum.DaDoc)
                        ^ value.Value;
            }
        }
        private bool? _daDuyet = true;
        public bool? DaDuyet
        {
            get => _daDuyet;
            set
            {
                _daDuyet = value;
                if (value == null)
                    filterList[1] = defaultFilter;
                else
                    filterList[1] = (item) =>
                        !item.StatusCode.HasFlag(CongVan.StatusCodeEnum.DaDuyet)
                        ^ value.Value;
            }
        }
        private bool? _daTiepNhan = true;
        public bool? DaTiepNhan
        {
            get => _daTiepNhan;
            set
            {
                _daTiepNhan = value;
                if (value == null)
                    filterList[2] = defaultFilter;
                else
                    filterList[2] = (item) =>
                        !item.StatusCode.HasFlag(CongVan.StatusCodeEnum.DaTiepNhan)
                        ^ value.Value;
            }
        }
        private bool? _daChuyen = true;
        public bool? DaChuyen
        {
            get => _daChuyen;
            set
            {
                _daChuyen = value;
                if (value == null)
                    filterList[3] = defaultFilter;
                else
                    filterList[3] = (item) =>
                        !(item.StatusCode.HasFlag(CongVan.StatusCodeEnum.DangChuyen)
                        && item.StatusCode.HasFlag(CongVan.StatusCodeEnum.DaDuyet))
                        ^ value.Value;
            }
        }
        #endregion
        //*/
        #region ButtonFilter
        private ICommand _buttonFilterCongVan;
        public ICommand ButtonFilterCongVan
        {
            get {
                if (_buttonFilterCongVan == null)
                    _buttonFilterCongVan = new RelayCommand(param => UpdateData(this, null));
                return _buttonFilterCongVan;
            }
        }
        #endregion

        #region
        private WindowsFormsHost _pdf;
        public WindowsFormsHost PDF { get => _pdf; set => _pdf = value; }
        #endregion
        public void ValueChanged(object sender, string[] args = null)
        {
            if (sender is CongVan)
            {
                CongVan target = sender as CongVan;
                target.NgayXuLi = System.DateTime.Now;
                // TODO: Update database
            }
        }
        public void UpdateData(object target, string[] args)
        {
            // Update this ViewModel
            OnPropertyChanged("DSCongVan");
            if (args == null)
                return;
        }

        private InboxLayoutViewModel()
        {
            PdfViewer pdf = new PdfViewer();
            PdfDocument pdfdoc = PdfDocument.Load(new MemoryStream(File.ReadAllBytes("C:/Users/longt/Downloads/Danh sách đề tài OOAD-46-62.pdf")));
            pdf.Document = pdfdoc;
            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = pdf;
            PDF = host;
            /*
            while (filterList.Count < filterList.Capacity)
                filterList.Add(defaultFilter);
            //*/
        }
        private static InboxLayoutViewModel _instance = null;

        public static InboxLayoutViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InboxLayoutViewModel();
                return _instance;
            }
        }

        #region COMMANDS
        public ICommand Open_ActionLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       ActionLayout actionLayout = new ActionLayout();
                       actionLayout.Show();
                   });
            }
        }
        #endregion
    }
}
