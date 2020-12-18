//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongVanManager
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using CongVanManager.View;
    using CongVanManager.ViewModel;

    public partial class User : ObservableObject
    {
        public User()
        {
            this.PhanHois = new HashSet<PhanHoi>();
        }

        public User(NguoiDung user)
        {
            Username = user.TenTaiKhoan;
            Password = user.MatKhau;
            LastSeen = user.LastSeen;
            //* TODO
            Loai = (UserType)user.LoaiNguoiDung;
            this.user = user;
            //*/
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public UserType Loai
        {
            get => _loai;
            set
            {
                _loai = value;
                OnPropertyChanged();
            }
        }
        public enum UserType : short
        {
            TruongPhong = 0,
            NhanVien = 1,
            Khach = 2,
            Unknown = 3
        }
        public NguoiDung user { get; set; }

        // show the time this user's last reloaded the database
        public DateTime LastSeen { get; set; }

        public virtual ICollection<PhanHoi> PhanHois { get; set; }

        public static void ReloadDatabase()
        {
            _db.CollectionChanged -= UserDBChanged;

            // Load Database from DataProvider
            User.DB.Clear();
            foreach (View.NguoiDung user in DataProvider.Ins.DB.NguoiDung)
                _db.Add(new User(user));

            // syncronize mechanic
            _db.CollectionChanged += UserDBChanged;
        }

        static void UserDBChanged(object sender, NotifyCollectionChangedEventArgs arg)
        {
            if (arg.Action == NotifyCollectionChangedAction.Move)
                return;
            if (arg.OldItems != null)
                foreach (User item in arg.OldItems)
                {
                    var cvs = DataProvider.Ins.DB.NguoiDung;
                    NguoiDung toRemove;
                    if (item.user != null)
                        toRemove = item.user;
                    else
                        toRemove = cvs.Find(item.Username);
                    cvs.Remove(toRemove);
                }
            if (arg.NewItems != null)
            {
                foreach (User item in arg.NewItems)
                    if (DB.Where(temp => item.Username == temp.Username) != null)
                    {
                        var temp = item.ToNguoiDung();
                        if (temp != null)
                        {
                            DataProvider.Ins.DB.NguoiDung.Add(temp);
                        }
                    }
                    else
                        Console.WriteLine("ERROR: Primary key duplication at User.");
            }
        }

        private static DelayedObservableCollection<User> _db;
        private string _username;
        private string _password;
        private UserType _loai;

        public static DelayedObservableCollection<User> DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new DelayedObservableCollection<User>();
                    ReloadDatabase();
                }
                return _db;
            }
            private set { }
        }

        public View.NguoiDung ToNguoiDung()
        {
            if (Loai == UserType.Unknown)
                return null;
            if (user != null)
                return user;
            return user = new View.NguoiDung
            {
                LastSeen = LastSeen,
                LoaiNguoiDung = (short)Loai,
                MatKhau = Password,
                TenTaiKhoan = Username
            };
        }
    }
}
