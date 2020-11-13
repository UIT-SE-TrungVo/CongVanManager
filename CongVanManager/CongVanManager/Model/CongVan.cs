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
        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        private string _soKyHieu;
        public string SoKyHieu
        {
            get => _soKyHieu;
            set { _soKyHieu = value; OnPropertyChanged(); }
        }
        private int? _soCongVan;
        public int? SoCongVan
        {
            get => _soCongVan;
            set { _soCongVan = value; OnPropertyChanged(); }
        }
        private DateTime? _ngayCongVan;
        public DateTime? NgayCongVan
        {
            get => _ngayCongVan;
            set { _ngayCongVan = value; OnPropertyChanged(); }
        }
        private string _trichYeu;
        public string TrichYeu
        {
            get => _trichYeu;
            set { _trichYeu = value; OnPropertyChanged(); }
        }
        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set { _ghiChu = value; OnPropertyChanged(); }
        }
        private string _pDFScanLocation;
        public string PDFScanLocation
        {
            get => _pDFScanLocation;
            set { _pDFScanLocation = value; OnPropertyChanged(); }
        }
        // WARNING: member not stored
        private DateTime _ngayXuLi;
        public DateTime NgayXuLi
        {
            get => _ngayXuLi;
            set { _ngayXuLi = value; OnPropertyChanged(); }
        }
        private StatusCodeEnum _statusCode;
        public StatusCodeEnum StatusCode
        {
            get => _statusCode;
            set { _statusCode = value; OnPropertyChanged(); }
        }
        // Status: 
        // 1: Đã tiếp nhận
        // 2: Đã duyệt
        // 4: Đang chuyển
        [Flags]
        public enum StatusCodeEnum : int
        {
            ChuaDoc = 0,
            DaTiepNhan = 1,
            DaDuyet_Den = 2,
            DaChuyen = 3,
            ChoDuyet = 4,
            DaDuyet_Di = 5,
            DaLuuTru = 6,
            DaGui = 7,
        }
        public string Status
        {
            get
            {
                if (StatusCode == StatusCodeEnum.DaDuyet_Den || 
                    StatusCode == StatusCodeEnum.DaDuyet_Di)
                    return "Đã duyệt";
                if (StatusCode == StatusCodeEnum.ChoDuyet)
                    return "Chờ duyệt";
                if (StatusCode == StatusCodeEnum.DaTiepNhan)
                    return "Đã tiếp nhận";
                if (StatusCode == StatusCodeEnum.ChuaDoc)
                    return "Chưa đọc";
                if (StatusCode == StatusCodeEnum.DaChuyen)
                    return "Đã chuyển";
                if (StatusCode == StatusCodeEnum.DaGui)
                    return "Đã gửi";
                if (StatusCode == StatusCodeEnum.DaLuuTru)
                    return "Đã lưu trữ";
                return "Không xác định";
            }
        }
        private LoaiCongVan _loaiCongVan;
        public virtual LoaiCongVan LoaiCongVan
        {
            get => _loaiCongVan;
            set { _loaiCongVan = value; OnPropertyChanged(); }
        }
        private LienHe _noiGui;
        public virtual LienHe NoiGui
        {
            get => _noiGui;
            set { _noiGui = value; OnPropertyChanged(); }
        }
        private ObservableCollection<NoiNhan> _danhSachNoiNhan;
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
 
        public static ObservableCollection<CongVan> DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new ObservableCollection<CongVan>();
                    // TODO: syncronize mechanic
                    _db.CollectionChanged += (obj, arg) => 
                    {
                        
                    };
                }
                return _db;
            }
            private set { }
        }
        // TODO: syncronize mechanic
    }
}
