using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CongVanManager.Command;
using CongVanManager.View;
using CongVanManager.View.usercontrol;

namespace CongVanManager.ViewModel
{
    class ActionLayoutViewModel : ObservableObject
    {
        public ActionLayout layout;
        public ActionLayoutViewModel()
        {
            isDuyetEnable = isGuiEnable = isXoaEnable = isEditEnable = false;
        }

        private bool _isDuyetEnable;
        public bool isDuyetEnable
        {
            get { return _isDuyetEnable; }
            set { _isDuyetEnable = value; OnPropertyChanged(); }
        }

        private bool _isXoaEnable;
        public bool isXoaEnable
        {
            get { return _isXoaEnable; }
            set { _isXoaEnable = value; OnPropertyChanged(); }
        }

        private bool _isEditEnable;
        public bool isEditEnable
        {
            get { return _isEditEnable; }
            set { _isEditEnable = value; OnPropertyChanged(); }
        }

        private bool _isGuiEnable;
        public bool isGuiEnable
        {
            get { return _isGuiEnable; }
            set { _isGuiEnable = value; OnPropertyChanged(); }
        }

        private DocType _Boxtype;

        public DocType BoxType
        {
            get { return _Boxtype; }
            set { _Boxtype = value; }
        }

        private CongVan _selectedCongVan;

        public CongVan selectedCongVan
        {
            get { return _selectedCongVan; }
            set {
                _selectedCongVan = value;
                if (_selectedCongVan == null)
                {
                    isDuyetEnable = isGuiEnable = isXoaEnable = isEditEnable = false;
                    return;
                }
                isDuyetEnable = value.StatusCode == CongVan.StatusCodeEnum.ChoDuyet || 
                    value.StatusCode == CongVan.StatusCodeEnum.DaTiepNhan;
                isGuiEnable = value.StatusCode == CongVan.StatusCodeEnum.DaDuyet_Den || 
                    value.StatusCode == CongVan.StatusCodeEnum.DaLuuTru;
                isEditEnable = value.StatusCode != CongVan.StatusCodeEnum.DaGui ||
                    value.StatusCode != CongVan.StatusCodeEnum.DaChuyen;
                isXoaEnable = MainWindowViewModel.Ins.User.Loai == User.UserType.TruongPhong;
            }
        }

        private static ActionLayoutViewModel _instance;
        public static ActionLayoutViewModel instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActionLayoutViewModel();
                return _instance;
            }
            private set { }
        }

        #region COMMANDS
        public ICommand DeleteCongVan
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       CongVan.DB.Remove(selectedCongVan);
                       layout?.Close();
                   });
            }
        }
        public ICommand EditCongVan
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       Page edit = new NewDocLayout(selectedCongVan);
                       MainWindowViewModel.Ins.SelectedPage = edit;
                       MainWindowViewModel.Ins.currentPageIndex = 6;
                       layout?.Close();
                   });
            }
        }
        public ICommand DuyetCongVan
        {
            get
            {
                return new RelayCommand(
                    x =>
                    {
                        //them phan quyen vao day
                        bool isSuccess = true;
                        var item = CongVan.DB.Where(cv => cv.Id == selectedCongVan.Id).First();
                        if (item != null)
                        {
                            if (BoxType == DocType.In)
                            {
                                item.StatusCode = CongVan.StatusCodeEnum.DaDuyet_Den;
                            }
                            else
                            {
                                if (BoxType == DocType.Out)
                                {
                                    item.StatusCode = CongVan.StatusCodeEnum.DaDuyet_Di;
                                }
                                else
                                {
                                    isSuccess = false;
                                }
                            }
                        }
                        else
                        {
                            isSuccess = false;
                        }
                        if(isSuccess)
                        {
                            MessageBox.Show("Đã duyệt thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không thành công!");
                        }
                        layout?.Close();
                    });
            }
        }
        public ICommand GuiCongVan
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       bool isSuccess = true;
                       var item = CongVan.DB.Where(cv => cv.Id == selectedCongVan.Id).First();
                       if (item != null)
                       {
                           if (BoxType == DocType.In)
                           {
                               item.StatusCode = CongVan.StatusCodeEnum.DaChuyen;
                           }
                           else
                           {
                               if (BoxType == DocType.Out)
                               {
                                   item.StatusCode = CongVan.StatusCodeEnum.DaGui;
                               }
                               else
                               {
                                   isSuccess = false;
                               }
                           }
                       }
                       else
                       {
                           isSuccess = false;
                       }
                       if (isSuccess)
                       {
                           MessageBox.Show("Đã gửi thành công!");
                       }
                       else
                       {
                           MessageBox.Show("Không thành công!");
                       }
                       layout?.Close();
                   });
            }
        }
        #endregion
    }
}
