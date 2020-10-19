﻿using CongVanManager.ViewModel;
using System;
using System.Collections.Generic;
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

namespace CongVanManager.View.usercontrol
{
    /// <summary>
    /// Interaction logic for uc_OutBoxFilter.xaml
    /// </summary>
    public partial class uc_OutBoxFilter : Page
    {
        public uc_OutBoxFilter()
        {
            InitializeComponent();
            this.DataContext = UC_OutBoxFilterViewModel.Ins;
        }
    }
}
