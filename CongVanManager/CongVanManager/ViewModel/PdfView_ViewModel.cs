using CongVanManager.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    public class PdfView_ViewModel : BaseViewModel
    {

        #region Bien
        private string _filename;

        public string filename
        {
            get { return _filename; }
            set { _filename = value; OnPropertyChanged(); }
        }

        private Stream _docStream;

        public Stream docStream
        {
            get { return _docStream; }
            set { _docStream = value; OnPropertyChanged(); }
        }

        #endregion
        public ICommand Click_ChonFileCommand { get; set; }

        public PdfView_ViewModel()
        {
            filename = "";
            //docStream = new FileStream(filename, FileMode.OpenOrCreate);
            Click_ChonFileCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF file (*.pdf) | *.pdf";
                if (openFileDialog.ShowDialog() == true)
                    filename = openFileDialog.FileName;
                docStream = new FileStream(filename, FileMode.Open);
            });
        }
    }
}