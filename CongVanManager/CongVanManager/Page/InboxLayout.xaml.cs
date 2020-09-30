using CongVanManager.ViewModel;
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
using PdfiumViewer;
using System.Windows.Forms;
using System.IO;

namespace CongVanManager
{
    /// <summary>
    /// Interaction logic for InboxLayout.xaml
    /// </summary>
    public partial class InboxLayout : Page
    {
        public InboxLayout()
        {
            InitializeComponent();
            this.DataContext = new InboxViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Integration.WindowsFormsHost host =
        new System.Windows.Forms.Integration.WindowsFormsHost();

            PdfViewer pdf = new PdfViewer();

            host.Child = pdf;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.pdfZone.Children.Add(host);
            PdfDocument pdfdoc = PdfDocument.Load(new MemoryStream(File.ReadAllBytes("D:/Document/Bảo trì/C2-NentangThaydoi(updated2).pdf")));
            pdf.Document = pdfdoc;
        }
    }
}
