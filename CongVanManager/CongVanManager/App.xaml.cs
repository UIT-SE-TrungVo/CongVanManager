using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CongVanManager.View;

namespace CongVanManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo ci = CultureInfo.GetCultureInfoByIetfLanguageTag("vi-VN");
            Thread.CurrentThread.CurrentCulture = ci;
        }

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            LoginLayout login = new LoginLayout();
            login.ShowDialog();
        }
    }
}
