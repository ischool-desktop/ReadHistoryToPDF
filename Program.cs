using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReadHistoryGenerator
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (MemoryStream licdata = new MemoryStream(Properties.Resources.Aspose_Total))
            {
                licdata.Seek(0, SeekOrigin.Begin);
                Aspose.Cells.License cell_lic = new Aspose.Cells.License();
                cell_lic.SetLicense(licdata);

                licdata.Seek(0, SeekOrigin.Begin);
                Aspose.Words.License word_lic = new Aspose.Words.License();
                word_lic.SetLicense(licdata);

                licdata.Seek(0, SeekOrigin.Begin);
                Aspose.Pdf.License pdf_lic = new Aspose.Pdf.License();
                pdf_lic.SetLicense(licdata);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
