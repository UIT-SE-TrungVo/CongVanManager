using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    class LoginLayoutViewModel : ObservableObject
    {
        LoginLayout window;
        PasswordBox passwordBox;
        public LoginLayoutViewModel(LoginLayout w, PasswordBox passwordBox)
        {
            window = w;
            this.passwordBox = passwordBox;
            //*
            Password = "admin";
            Username = "admin";
            //*/
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        
        public string Password
        {
            get => passwordBox.Password;
            set
            {
                passwordBox.Password = value;
                OnPropertyChanged();
            }
        }

        public ICommand Login
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       if (User.DB.FirstOrDefault(user => user.Username == Username)?.Password != Password)
                       {
                           // TODO: better message box
                           MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai.");
                           return;
                       }
                       MainWindowViewModel.Ins.SetUser(Username);
                       MainWindow mainWindow = new MainWindow();
                       mainWindow.Show();
                       window.Close();
                   });
            }
        }
    }
}
