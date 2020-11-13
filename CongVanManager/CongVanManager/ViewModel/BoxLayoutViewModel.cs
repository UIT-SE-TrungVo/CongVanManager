﻿using System;
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
    class BoxLayoutViewModel : ObservableObject
    {
        #region DanhSachCongVan
        public ICollection<CongVan> DSCongVan
        {
            get { return CongVan.DB.Where(Filter).ToList(); }
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
                OnPropertyChanged();
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
        public string SelectedCongVanSentDate
        {
            get
            {
                //*
                if (_selectedCongVan?.NgayCongVan == null)
                    return "";
                else
                    return string.Format("{0:HH:mm} ngày {0:dd-MM-yyyy}", _selectedCongVan.NgayCongVan);
                /*/
                return _selectedCongVan?.NgayCongVan;
                //*/
            }
            set
            {
                OnPropertyChanged();
                /*
                if (_selectedCongVan == null)
                    return;
                if (SelectedCongVanSentDate != value)
                {
                    _selectedCongVan.NgayCongVan = value;
                    ValueChanged(_selectedCongVan);
                }
                //*/
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

        #region FilterSetting
        private bool Filter(CongVan item)
        {
            return (UCFilter.DataContext as FilterSetting).Filter(item);
        }
        #endregion

        #region ButtonFilter
        private ICommand _buttonFilterCongVan;
        public ICommand ButtonFilterCongVan
        {
            get
            {
                if (_buttonFilterCongVan == null)
                    _buttonFilterCongVan = new RelayCommand(param => UpdateData(this, null));
                return _buttonFilterCongVan;
            }
        }
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

        #region BOX TYPE SWITCHER VARIABLE
        public DocType BoxType;
        public int iDocType
        {
            get;
            set;
        }
        private Page _ucfilter;
        public Page UCFilter
        {
            get
            {
                return _ucfilter;
            }
            set
            {
                _ucfilter = value; OnPropertyChanged();
            }
        }
        #endregion

        private BoxLayoutViewModel(DocType docType)
        {
            BoxType = docType;
            iDocType = (int)BoxType;
            switch (docType)
            {
                case DocType.In:
                    UCFilter = new uc_InBoxFilter();
                    break;
                case DocType.Out:
                    UCFilter = new uc_OutBoxFilter();
                    break;
            }
            
            //[TESTING: avoid input for doctype.out]
            if (BoxType == DocType.Out) return;
        }

        private static BoxLayoutViewModel ins_in = null;
        private static BoxLayoutViewModel ins_out = null;
        public static BoxLayoutViewModel Ins(DocType doc)
        {
            if (doc == DocType.In)
            {
                if (ins_in == null)
                    ins_in = new BoxLayoutViewModel(doc);
                return ins_in;
            }
            if (doc == DocType.Out)
            {
                if (ins_out == null)
                    ins_out = new BoxLayoutViewModel(doc);
                return ins_out;
            }
            return null;
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