using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    public class ContactLayoutViewModel : ObservableObject
    {
        private ObservableCollection<LienHe> _DSLienHe = new ObservableCollection<LienHe>();

        public ObservableCollection<LienHe> DSLienHe
        {
            get { return _DSLienHe; }
            set { _DSLienHe = value; OnPropertyChanged(); }
        }

        #region Binding
        private bool _IsDialogOpen;

        public bool IsDialogOpen
        {
            get { return _IsDialogOpen; }
            set { _IsDialogOpen = value; OnPropertyChanged(); }
        }
        private string _newEmail;

        public string newEmail
        {
            get { return _newEmail; }
            set { _newEmail = value; OnPropertyChanged(); }
        }

        private string _newTen;

        public string newTen
        {
            get { return _newTen; }
            set { _newTen = value; OnPropertyChanged(); }
        }

        private string _type;

        public string type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        //chinh sua

        private bool _IsEnable = true;

        public bool IsEnable
        {
            get { return _IsEnable; }
            set { _IsEnable = value; OnPropertyChanged(); }
        }

        private LienHe _SelectedLienHe;

        public LienHe SelectedLienHe
        {
            get { return _SelectedLienHe; }
            set { _SelectedLienHe = value; OnPropertyChanged(); if (_SelectedLienHe != null) { IsEnable = true; } else { IsEnable = false; } }
        }

        private LienHe ChonLienHe;

        public ContactLayoutViewModel()
        {
            ResetListLienHe();
        }

        public void ResetListLienHe()
        {
            SelectedLienHe = null;
            if (DSLienHe.Count != 0)
                DSLienHe.Clear();
            foreach (LienHe item in LienHe.DB)
            {
                DSLienHe.Add(item);
            }
        }
        #endregion

        public async Task<LienHe> getSelectedLienHe()
        {
            return ChonLienHe;
        }

        #region Command
        public ICommand XacNhan
        {
            get
            {
                return new RelayCommand<Window>(
                x =>
                {
                    ChonLienHe = SelectedLienHe;
                    x.Close();
                });
            }
        }
        public ICommand Close
        {
            get
            {
                return new RelayCommand<Window>(
                x =>
                {
                    x.Close();
                });
            }
        }

        public ICommand Save
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    if (String.IsNullOrWhiteSpace(newEmail) || String.IsNullOrWhiteSpace(newTen))
                        return;
                    if (type == "Nhập liên hệ mới")
                    {
                        LienHe lh = new LienHe() { Email = newEmail, Name = newTen };
                        if(LienHe.DB.Where(l => l.Email == newEmail).Count() > 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Email đã tồn tại.");
                            return;
                        }    
                        LienHe.DB.Add(lh);
                        ResetListLienHe();
                    }
                    else
                    {
                        if (type == "Sửa tên liên hệ")
                        {
                            LienHe lh = LienHe.DB[LienHe.DB.IndexOf(LienHe.DB.Where(l => l.Email == newEmail)?.First())];
                            lh.Name = newTen;
                            DataProvider.Ins.DB.LienHe.Find(newEmail).TenLienHe = newTen;
                            DataProvider.Ins.DB.SaveChanges();
                            ResetListLienHe();
                        }
                    }
                    IsDialogOpen = false;
                    IsEnable = false;
                    newTen = "";
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
                    newEmail = "";
                    newTen = "";
                    IsDialogOpen = false;
                    IsEnable = true;
                });
            }
        }

        public ICommand ThemLienHe
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    ShowDialogAdd();
                });
            }
        }

        public ICommand SuaLienHe
        {
            get
            {
                return new RelayCommand(
                x =>
                {
                    ShowDialogEdit();
                });
            }
        }
        #endregion
        public void ShowDialogEdit()
        {
            IsEnable = false;
            IsDialogOpen = true;
            type = "Sửa tên liên hệ";
            newTen = SelectedLienHe.Name;
            newEmail = SelectedLienHe.Email;
        }
        public void ShowDialogAdd()
        {
            IsEnable = true;
            IsDialogOpen = true;
            type = "Nhập liên hệ mới";
        }
    }
}
