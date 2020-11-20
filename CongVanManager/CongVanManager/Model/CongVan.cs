namespace CongVanManager
{
    using CongVanManager.ViewModel;
    using CongVanManager.View;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class CongVan : ObservableObject
    {
        public CongVan()
        {
            this.DanhSachNoiNhan = new ObservableCollection<NoiNhan>();
            this.PhanHois = new ObservableCollection<PhanHoi>();
        }

        public CongVan(in View.CongVan cv)
        {
            Id = cv.MaCongVan;
            StatusCode = (StatusCodeEnum)cv.TinhTrang.GetValueOrDefault(0);
            GhiChu = cv.GhiChu;

            LoaiCongVan = LoaiCongVan.Get(cv.LoaiCongVan);
            if (LoaiCongVan == null)
                LoaiCongVan.DB.Add(new LoaiCongVan(cv.LoaiCongVan));
            LoaiCongVan.CongVanDaGui.Add(this);

            NgayCongVan = cv.NgayCongVan;
            NgayXuLi = cv.NgayXuLi;
            
            NoiGui = LienHe.Get(cv.LienHe);
            if (NoiGui == null)
                LienHe.DB.Add(new LienHe(cv.LienHe));
            NoiGui.CongVans.Add(this);

            PDFScanLocation = cv.PDFScan;

            PhanHois = new ObservableCollection<PhanHoi>();

            SoCongVan = cv.SoCongVan;
            SoKyHieu = cv.SoKyHieu;
            TrichYeu = cv.TrichYeu;

            DanhSachNoiNhan = new ObservableCollection<NoiNhan>();
            foreach (View.LienHe lh in cv.LienHes)
            {
                LienHe lienHe = LienHe.Get(lh);
                if (lienHe == null)
                    LienHe.DB.Add(new LienHe(lh));
                NoiNhan noiNhan = new NoiNhan{
                    CongVan = this,
                    LienHe = lienHe
                };
                lienHe.DanhSachNoiNhan.Add(noiNhan);
                DanhSachNoiNhan.Add(noiNhan);
                NoiNhan.DB.Add(noiNhan);
            }
        }

        public View.CongVan ToCongVan()
        {
            return new View.CongVan
            {
                MaCongVan = Id,
                GhiChu = GhiChu,
                NgayCongVan = NgayCongVan,
                NoiGui = NoiGui.Email,
                MaLoaiCongVan = LoaiCongVan.Id,
                NgayXuLi = NgayXuLi,
                PDFScan = PDFScanLocation,
                SoCongVan = SoCongVan,
                SoKyHieu = SoKyHieu,
                TinhTrang = (int)StatusCode,
                TrichYeu = TrichYeu
            };
        }

        private long _id;
        public long Id
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

                    // Load Database from DataProvider
                    foreach (View.CongVan cv in DataProvider.Ins.DB.CongVan)
                        _db.Add(new CongVan(cv));

                    // syncronize mechanic
                    _db.CollectionChanged += (obj, arg) =>
                    {
                        if (arg.Action == NotifyCollectionChangedAction.Move)
                            return;
                        if (arg.OldItems != null)
                            foreach (CongVan item in arg.OldItems)
                            {
                                var cvs = DataProvider.Ins.DB.CongVan;
                                cvs.Remove(cvs.Find(item.Id));
                            }
                        if (arg.NewItems != null)
                            foreach (CongVan item in arg.NewItems)
                            {
                                item.NgayXuLi = DateTime.Now;
                                DataProvider.Ins.DB.CongVan.Add(item.ToCongVan());
                            }
                    };
                }
                return _db;
            }
            private set { }
        }
        // TODO: syncronize mechanic
        public static CongVan Get(View.CongVan cv)
        {
            foreach (CongVan item in DB)
            {
                if (item.Id == cv.MaCongVan)
                    return item;
            }
            return null;
        }
    }
}
