namespace CongVanManager
{
    using CongVanManager.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public partial class CongVan : ObservableObject
    {
        public CongVan()
        {
            this.DanhSachNoiNhan = new ObservableCollection<NoiNhan>();
            this.PhanHois = new ObservableCollection<PhanHoi>();
        }

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        public string SoKyHieu
        {
            get => _soKyHieu;
            set { _soKyHieu = value; OnPropertyChanged(); }
        }
        public int? SoCongVan
        {
            get => _soCongVan;
            set { _soCongVan = value; OnPropertyChanged(); }
        }
        public DateTime? NgayCongVan
        {
            get => _ngayCongVan;
            set { _ngayCongVan = value; OnPropertyChanged(); }
        }
        public string TrichYeu
        {
            get => _trichYeu;
            set { _trichYeu = value; OnPropertyChanged(); }
        }
        public string GhiChu
        {
            get => _ghiChu;
            set { _ghiChu = value; OnPropertyChanged(); }
        }
        public string PDFScanLocation
        {
            get => _pDFScanLocation;
            set { _pDFScanLocation = value; OnPropertyChanged(); }
        }
        public DateTime NgayXuLi
        {
            get => _ngayXuLi;
            set { _ngayXuLi = value; OnPropertyChanged(); }
        }

        public int StatusCode
        {
            get => _statusCode;
            set { _statusCode = value; OnPropertyChanged(); }
        }
        // Status: 
        // 1: Đã tiếp nhận
        // 2: Đã duyệt
        // 4: Đang chuyển
        public string Status
        {
            get
            {
                if (StatusCode >= 6) return "Đã chuyển";
                if (StatusCode >= 2) return "Đã duyệt";
                if (StatusCode == 1) return "Đã tiếp nhận";
                else return "Chưa đọc";
            }
        }

        public virtual LoaiCongVan LoaiCongVan
        {
            get => _loaiCongVan;
            set { _loaiCongVan = value; OnPropertyChanged(); }
        }
        public virtual LienHe NoiGui
        {
            get => _noiGui;
            set { _noiGui = value; OnPropertyChanged(); }
        }
        public virtual ObservableCollection<NoiNhan> DanhSachNoiNhan { get => _danhSachNoiNhan;
            set {
                value.CollectionChanged +=
                (object sender, NotifyCollectionChangedEventArgs e)
                => { this.OnPropertyChanged(); };
                _danhSachNoiNhan = value;
                OnPropertyChanged(); }
        }
        public virtual ObservableCollection<PhanHoi> PhanHois { get; set; }

        public CongVan Clone
        {
            get { return MemberwiseClone() as CongVan; }
        }

        private static ObservableCollection<CongVan> _db;
        private int _id;
        private string _soKyHieu;
        private int? _soCongVan;
        private DateTime? _ngayCongVan;
        private string _trichYeu;
        private string _ghiChu;
        private string _pDFScanLocation;
        private DateTime _ngayXuLi;
        private int _statusCode;
        private LoaiCongVan _loaiCongVan;
        private LienHe _noiGui;
        private ObservableCollection<NoiNhan> _danhSachNoiNhan;

        public static ObservableCollection<CongVan> DB
        {
            get
            {
                if (_db == null)
                    _db = new ObservableCollection<CongVan>();
                return _db;
            }
            private set { }
        }
    }
}
