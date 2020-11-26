using CongVanManager.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CongVanManager.View;

namespace CongVanManager.ViewModel
{
    class SettingLayoutViewModel : ObservableObject
    {
        private SettingLayoutViewModel()
        {
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

        public ICommand RemovwUser
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       User.DB.Remove(selectedUser);
                       OnPropertyChanged("UserList");
                   });
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
    }
}
