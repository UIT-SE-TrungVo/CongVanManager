using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CongVanManager.Command;
using CongVanManager.View;

public enum PageName { InboxLayout, NewDocLayout_Chooser, NewDocLayout_In, NewDocLayout_Out, SettingLayout, OutboxLayout, ReportLayout};

namespace CongVanManager.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private User user;
        // TODO: set this field
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
                OnPropertyChanged("isSettingEnable");
                OnPropertyChanged("Username");
            }
        }
        public void SetUser(string username)
        {
            User = User.DB.First(item => item.Username == username);
        }

        private readonly Page[] page = {    new BoxLayout(DocType.In), //0
                                            new NewDocLayout_Chooser(), //1
                                            new NewDocLayout(DocType.In), //2
                                            new NewDocLayout(DocType.Out), //3
                                            new SettingLayout(), //4
                                            new BoxLayout(DocType.Out), //5
                                            new ReportLayout(), //6
        };

        private Page _selectedPage;
        public Page SelectedPage
        {
            get {
                return _selectedPage;
            }
            set {
                _selectedPage = value; OnPropertyChanged();
            }
        }

        private MainWindowViewModel()
        {
            ChangePage(0);

            Setting.Ins.PropertyChanged += (obj, arg) => 
            {
                if (arg.PropertyName == "TruongPhong")
                    OnPropertyChanged("isSettingEnable");
            };

            #region DATA SAMPLE BINDING (DISABLED)
            /*
            User user1 = new User
            {
                LastSeen = DateTime.Now,
                Loai = (int) User.UserType.TruongPhong,
                Username = "admin",
                Password = "admin"
            };
            User user2 = new User
            {
                LastSeen = DateTime.Now,
                Loai = (int)User.UserType.NhanVien,
                Username = "nv",
                Password = "nv"
            };
            User user3 = new User
            {
                LastSeen = DateTime.Now,
                Loai = (int)User.UserType.Khach,
                Username = "guest",
                Password = "guest"
            };
            LienHe contact = new LienHe
            {
                Name = "Phòng Đào tạo",
                Email = "phongdaotao@gmail.com"
            };
            NoiNhan noiNhan1 = new NoiNhan
            {
                LienHe = contact
            };
            NoiNhan noiNhan2 = new NoiNhan
            {
                LienHe = contact
            };
            LoaiCongVan loaiCongVan = new LoaiCongVan { Id = "Kế hoạch" };
            CongVan congVan = new CongVan
            {
                Id = 0,
                LoaiCongVan = loaiCongVan,
                TrichYeu = "Ngày hội qua môn cho sinh viên năm 6",
                SoKyHieu = "01/ĐH-CNTT",
                NoiGui = contact,
                DanhSachNoiNhan = new DelayedObservableCollection<NoiNhan> {
                    noiNhan1, noiNhan2 },
                SoCongVan = 1,
                NgayCongVan = System.DateTime.Now,
                GhiChu = "Đme ngày hội xàm vài lòn",
                StatusCode = CongVan.StatusCodeEnum.DaGui,
                NgayXuLi = System.DateTime.Now
            };
            CongVan congVan2 = new CongVan
            {
                Id = 1,
                LoaiCongVan = loaiCongVan,
                TrichYeu = "Ngày hội chia sẻ cách để viết một xâu vô cùng dài trong tựa đề chỉ vì lý do kiểm thử phần mềm cho sinh viên năm 6, 7, 8, 9, thực ra ai cũng vô được",
                SoKyHieu = "02/ĐH-CNTT",
                NoiGui = contact,
                DanhSachNoiNhan = new DelayedObservableCollection<NoiNhan> {
                    noiNhan1},
                SoCongVan = 2,
                NgayCongVan = System.DateTime.Now,
                GhiChu = "Chỉ là để chia sẻ cách để viết một xâu vô cùng dài trong tựa đề chỉ vì lý do kiểm thử phần mềm cho sinh viên năm 6, 7, 8, 9, thực ra ai cũng vô được mời mọi người cùng vô cho nó vui",
                StatusCode = CongVan.StatusCodeEnum.DaDuyet_Den,
                NgayXuLi = System.DateTime.Now
            };
            noiNhan1.CongVan = noiNhan2.CongVan = congVan;

            CongVan.DB.Add(congVan);
            CongVan.DB.Add(congVan2);
            LoaiCongVan.DB.Add(loaiCongVan);
            NoiNhan.DB.Add(noiNhan2);
            NoiNhan.DB.Add(noiNhan1);
            LienHe.DB.Add(contact);

            //User.DB.Add(user1);
            //User.DB.Add(user2);
            //User.DB.Add(user3);
            //Setting.Ins.TruongPhong = user1;
            //Setting.Ins.LastUpdated = DateTime.Now;

            //*/
            #endregion

            CongVan.DB.CollectionChanged +=
                (sender, e)
                =>
                { OnPropertyChanged("DSCongVan"); };
        }

        private static MainWindowViewModel _instance = null;
        public static MainWindowViewModel Ins
        {
            get {
                if (_instance == null)
                    _instance = new MainWindowViewModel();
                return _instance;
            }
            private set { }
        }

        public int currentPageIndex = -1;
        void ChangePage(int pageNumber)
        {
            if (pageNumber == currentPageIndex) return;
            SelectedPage = page[pageNumber];
            currentPageIndex = pageNumber;
        }

        public void PageSwitch(PageName messageID)
        {
            if (messageID >= 0 && (int)messageID < page.Length)
            {
                ChangePage((int)messageID);
            }
        }

        //COMMANDS
        public ICommand Open_NewDocLayout_Chooser
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       NewDocLayout_ChooserViewModel.instance.SetPreviousPage(currentPageIndex);
                       MainWindowViewModel.Ins.PageSwitch(PageName.NewDocLayout_Chooser);
                   });
            }
        }

        public ICommand Open_InboxLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.Ins.PageSwitch(PageName.InboxLayout);
                   });
            }
        }

        public ICommand Open_OutboxLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.Ins.PageSwitch(PageName.OutboxLayout);
                   });
            }
        }

        public ICommand Open_ReportLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.Ins.PageSwitch(PageName.ReportLayout);
                   });
            }
        }

        public ICommand Open_SettingLayout
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       MainWindowViewModel.Ins.PageSwitch(PageName.SettingLayout);
                   });
            }
        }

        public ICommand Logout
        {
            get
            {
                return new RelayCommand<MainWindow>(
                    x=>
                    {
                        x.Visibility = Visibility.Collapsed;
                        LoginLayout login = new LoginLayout();
                        login.ShowDialog();
                        x.Visibility = Visibility.Visible;
                    });
            }
        }

        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                OnPropertyChanged();
            }
        }

        public bool isSettingEnable
        {
            get => User == Setting.Ins.TruongPhong && User != null;
            set
            {
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => User?.Username;
            set
            {
                OnPropertyChanged();
            }
        }

        private string filterText;
        
        #region ButtonFilter
        private ICommand _buttonFilterCongVan;
        public ICommand ButtonFilterCongVan
        {
            get
            {
                if (_buttonFilterCongVan == null)
                    _buttonFilterCongVan = new RelayCommand(param => 
                    {
                        (page[0].DataContext as BoxLayoutViewModel).UpdateData(this, null);
                        (page[5].DataContext as BoxLayoutViewModel).UpdateData(this, null);
                    });
                return _buttonFilterCongVan;
            }
        }
        #endregion
    }
}
