using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    class LoginLayoutViewModel
    {
        LoginLayout window;
        public LoginLayoutViewModel(LoginLayout w)
        {
            window = w;
        }

        public ICommand Login
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
    }
}
