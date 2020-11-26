using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    class UserDetailLayoutViewModel : ObservableObject
    {
        public PasswordBox passwordBox;

        private User user;
        private string username;
        private User.UserType userType = User.UserType.Khach;

        UserDetailLayout window;
        public UserDetailLayoutViewModel(UserDetailLayout w, User user, PasswordBox passwordBox)
        {
            window = w;
            this.passwordBox = passwordBox;

            Type_NameDictionary = new Dictionary<User.UserType, string>
            {
                {User.UserType.TruongPhong, "Trưởng phòng" },
                {User.UserType.NhanVien, "Nhân viên" },
                {User.UserType.Khach, "Khách" },
                {User.UserType.Unknown, "" }
            };

            User = user;
            if (User != null)
            {
                Username = User.Username;
                Password = User.Password;
                UserType = User.Loai;
            }
        }

        public ICommand Save
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       //save before close
                       // Trùng tên người dùng
                       if (User.DB.FirstOrDefault(item=>item.Username == Username) != User)
                       {
                           MessageBox.Show("Tên người dùng mới trùng với tên người dùng" +
                               "cũ.\nVui Lòng chọn tên người dùng khác.", "Lỗi: Trùng tên");
                           return;
                       }

                       // Người dùng mới là trưởng phòng.
                       if (UserType == (short)User.UserType.TruongPhong &&
                       User?.Loai != User.UserType.TruongPhong)
                       {
                           MessageBoxResult result = MessageBox.Show("Bạn có muốn cấp quyền" +
                               "trưởng phòng cho người dùng này không?\n" +
                               "Bạn sẽ mất quyền trưởng phòng.\n" +
                               "Bạn sẽ tạm thời vẫn có thể sửa quyền trong tab này.",
                               "Đổi quyền trưởng phòng?", MessageBoxButton.YesNo);
                           if (result != MessageBoxResult.Yes)
                               return;

                           Setting.Ins.TruongPhong.Loai = User.UserType.NhanVien;
                       }

                       if (User != null)
                       {
                           if (User.Loai == User.UserType.TruongPhong &&
                           UserType != User.UserType.TruongPhong)
                           {
                               MessageBox.Show("Cần có ít nhất 1 trưởng phòng.\n" +
                                   "Vui lòng chuyển quyền trưởng phòng cho người dùng" +
                                   "khác.\n Bạn sẽ tạm thời vẫn có thể sửa quyền trong tab này.",
                                   "Cần có Trưởng Phòng");
                               return;
                           }
                           User.DB.Remove(User);
                       }

                       User newUser = new User
                       {
                           Username = Username,
                           Password = Password,
                           Loai = UserType,
                           LastSeen = DateTime.Now
                       };
                       User.DB.Add(newUser);
                       if (newUser.Loai == User.UserType.TruongPhong)
                           Setting.Ins.TruongPhong = newUser;

                       window.Close();
                   });
            }
        }

        public ICommand Reset
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       UserType = User.UserType.Unknown;
                       Password = "";
                       Username = "";
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
                       window.Close();
                   });
            }
        }
        
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
                OnPropertyChanged("CanEditUsername");
            }
        }
        public bool CanEditUsername
        {
            get => User == null;
        }

        public string Username
        {
            get => username; set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => passwordBox.Password; set
            {
                passwordBox.Password = value;
                OnPropertyChanged();
            }
        }
        public User.UserType UserType
        {
            get => userType; set
            {
                userType = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<User.UserType, string> Type_NameDictionary
        {
            get => type_NameDictionary; set
            {
                type_NameDictionary = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<User.UserType, string> type_NameDictionary;
    }    
}
