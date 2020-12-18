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

        private static bool IsEqual(byte[] one, byte[] other)
        {
            if (one.Length != other.Length)
                return false;

            int len = one.Length;
            for (int i = 0; i < len; i++)
                if (one[i] != other[i])
                    return false;
            return true;
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

                var dbItem = DataProvider.Ins.DB.PDFScans.Find(FileName);
                // If the item is in the DB and the content is different
                if (dbItem != null)
                {
                    if (!IsEqual(dbItem.Content, pdf.Content))
                    {
                        PublishPDF(ref FilePath);
                        return;
                    }
                    else
                    {
                        FilePath = FileName;
                        return;
                    }
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
