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

        public ActionLayoutViewModel()
        {
            isDuyetEnable = false;
            isGuiEnable = false;
        }

        private bool _isDuyetEnable;

        public bool isDuyetEnable
        {
            get { return _isDuyetEnable; }
            set { _isDuyetEnable = value; OnPropertyChanged(); }
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
                if(value.StatusCode == CongVan.StatusCodeEnum.ChoDuyet || value.StatusCode == CongVan.StatusCodeEnum.ChuaDoc)
                {
                    isDuyetEnable = true;
                }
                if(value.StatusCode == CongVan.StatusCodeEnum.DaDuyet_Den || value.StatusCode == CongVan.StatusCodeEnum.DaLuuTru )
                {
                    isGuiEnable = true;
                }
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
                       //them phan quyen o day
                       CongVan.DB.Remove(selectedCongVan);
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
                       MessageBox.Show("EDIT" + selectedCongVan.Id);
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
                               item.StatusCode = CongVan.StatusCodeEnum.DaGui;
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
                   });
            }
        }
        #endregion
    }
}
