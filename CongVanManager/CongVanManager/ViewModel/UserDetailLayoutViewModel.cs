using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    class UserDetailLayoutViewModel
    {
        UserDetailLayout window;
        public UserDetailLayoutViewModel(UserDetailLayout w)
        {
            window = w;
        }

        public ICommand Save
        {
            get
            {
                return new RelayCommand(
                   x =>
                   {
                       //save before close
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
                       //reset everything
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
    }    
}
