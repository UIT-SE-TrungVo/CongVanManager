using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager
{
    class PDFDownloader
    {
        // Load from Database. return whether operation success
        public static bool LoadPDF(string FileName)
        {
            try
            {
                PDFScan pdf = DataProvider.Ins.DB.PDFScans.Find(FileName);

                if (pdf == null)
                    return false;

                using (FileStream fw = File.OpenWrite(pdf.PDFName))
                {
                    byte[] data = pdf.Content;
                    fw.Write(data, 0, data.Length);
                    fw.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        // WARNING: FilePath changed.
        public static void PublishPDF(ref string FilePath, string FileName = null)
        {
            if (FileName == null)
                FileName = Guid.NewGuid().ToString() + ".pdf";

            PDFScan pdf = new PDFScan { PDFName = FileName };

            try
            {
                using (FileStream fs = File.OpenRead(FilePath))
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    pdf.Content = data;
                    fs.Close();
                }

                if (DataProvider.Ins.DB.PDFScans.Find(FileName) != null)
                {
                    PublishPDF(ref FilePath);
                    return;
                }

                DataProvider.Ins.DB.PDFScans.Add(pdf);

                FilePath = FileName;

                using (FileStream fw = File.OpenWrite(FilePath))
                {
                    byte[] data = pdf.Content;
                    fw.Write(data, 0, data.Length);
                    fw.Close();
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
    }
}
