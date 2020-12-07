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

        private LienHe _SelectedLienHe;

        public LienHe SelectedLienHe
        {
            get { return _SelectedLienHe; }
            set { _SelectedLienHe = value; OnPropertyChanged(); }
        }

        private LienHe ChonLienHe;

        public ContactLayoutViewModel()
        {
            ResetListLienHe();
        }

        public void ResetListLienHe()
        {
            if (DSLienHe.Count != 0)
                DSLienHe.Clear();
            foreach (LienHe item in LienHe.DB)
            {
                DSLienHe.Add(item);
            }
        }

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
        #endregion
    }
}
