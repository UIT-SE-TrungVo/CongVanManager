﻿using CongVanManager.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace CongVanManager.View
{
    /// <summary>
    /// Interaction logic for ReportLayout.xaml
    /// </summary>
    public partial class ReportLayout : Page
    {
        public ReportLayout()
        {
            InitializeComponent();
            this.DataContext = new ReportLayoutViewModel();
        }
    }
}
