using CongVanManager.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CongVanManager.View;
using System.Windows;

namespace CongVanManager.ViewModel
{
    class SettingLayoutViewModel : ObservableObject
    {
        private SettingLayoutViewModel()
        {
            SelectedTruongPhong = Setting.Ins.TruongPhong;
            User.DB.CollectionChanged += (obj, arg) => { SelectedTruongPhong = Setting.Ins.TruongPhong; };
        }

        private static SettingLayoutViewModel _instance;
        public static SettingLayoutViewModel instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingLayoutViewModel();
                return _instance;
            }
            private set { }
        }

        public ICommand CreateNewUser
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       UserDetailLayout window = new UserDetailLayout();
                       window.ShowDialog();
                   });
            }
        }

        public ICommand EditUser
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       UserDetailLayout window = new UserDetailLayout(selectedUser);
                       window.ShowDialog();
                   });
            }
        }

        public ICommand RemoveUser
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       if (selectedUser != Setting.Ins.TruongPhong)
                           User.DB.Remove(selectedUser);
                       else
                           MessageBox.Show("Cần có ít nhất 1 trưởng phòng.\n" +
                               "Vui lòng chuyển quyền trưởng phòng cho người dùng" +
                               "khác.\n", "Cần có Trưởng Phòng");
                       OnPropertyChanged("UserList");
                   });
            }
        }

        public ICommand ConfirmTruongPhong
        {
            get
            {
                return new RelayCommand(
                    x =>
                    {
                        if (SelectedTruongPhong != null)
                        {
                            //save before close
                            // Trùng tên người dùng
                            if (SelectedTruongPhong == Setting.Ins.TruongPhong)
                            {
                                return;
                            }

                            MessageBoxResult result = MessageBox.Show("Bạn có muốn cấp quyền" +
                                "trưởng phòng cho người dùng này không?\n" +
                                "Trưởng phòng hiện tại sẽ mất quyền trưởng phòng.\n" +
                                "Bạn sẽ tạm thời vẫn có thể sửa quyền trong tab này.",
                                "Đổi quyền trưởng phòng?", MessageBoxButton.YesNo);
                            if (result != MessageBoxResult.Yes)
                                return;

                            Setting.Ins.TruongPhong.Loai = User.UserType.NhanVien;
                            SelectedTruongPhong.Loai = User.UserType.TruongPhong;

                            Setting.Ins.TruongPhong = SelectedTruongPhong;
                        }
                    });
            }
        }
        public User SelectedTruongPhong
        {
            get
            {
                return _selectedTruongPhong;
            }
            set
            {
                _selectedTruongPhong = value;
                OnPropertyChanged();
            }
        }

        public DelayedObservableCollection<User> UserList
        {
            get
            {
                return User.DB;
            }
            set
            {
                SelectedTruongPhong = Setting.Ins.TruongPhong;
                OnPropertyChanged();
            }
        }

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        private User selectedUser;
        private User _selectedTruongPhong;
    }
}
