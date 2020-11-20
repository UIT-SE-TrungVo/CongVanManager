using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CongVanManager.Command;
using CongVanManager.View;

public enum PageName { InboxLayout, NewDocLayout_Chooser, NewDocLayout_In, NewDocLayout_Out, SettingLayout, OutboxLayout};

namespace CongVanManager.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly Page[] page = {    new BoxLayout(DocType.In), //0
                                            new NewDocLayout_Chooser(), //1
                                            new NewDocLayout(DocType.In), //2
                                            new NewDocLayout(DocType.Out), //3
                                            new SettingLayout(), //4
                                            new BoxLayout(DocType.Out) //5
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

            #region DATA SAMPLE BINDING (DISABLED)
            /*
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
                DanhSachNoiNhan = new ObservableCollection<NoiNhan> {
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
                DanhSachNoiNhan = new ObservableCollection<NoiNhan> {
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

            CongVan.DB.CollectionChanged +=
                (sender, e)
                =>
                { OnPropertyChanged("DSCongVan"); };
            //*/
            #endregion
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

        private int currentPageIndex = -1;
        void ChangePage(int pageNumber)
        {
            if (pageNumber == currentPageIndex) return;
            SelectedPage = page[pageNumber];
            currentPageIndex = pageNumber;
        }

        public void PageSwitch(PageName messageID)
        {
            switch (messageID)
            {
                case PageName.InboxLayout:
                    ChangePage(0);
                    break;
                case PageName.NewDocLayout_Chooser:
                    ChangePage(1);
                    break;
                case PageName.NewDocLayout_In:
                    ChangePage(2);
                    break;
                case PageName.NewDocLayout_Out:
                    ChangePage(3);
                    break;
                case PageName.SettingLayout:
                    ChangePage(4);
                    break;
                case PageName.OutboxLayout:
                    ChangePage(5);
                    break;
                default:
                    break;
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

        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
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
