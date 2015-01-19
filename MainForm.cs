using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using Aspose.Cells;
using System.Xml.XPath;
using System.Diagnostics;
using Aspose.Words.MailMerging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ReadHistoryGenerator
{
    public partial class MainForm : Form
    {
        public static string AppPath = Application.StartupPath;

        public MainForm()
        {
            InitializeComponent();
        }

        public static string GetPath(string folderName)
        {
            return System.IO.Path.Combine(AppPath, folderName);
        }

        /// <summary>
        /// 例：103005_許博宇_05
        /// </summary>
        /// <returns></returns>
        public static string GetMainFN(string stu_number, string stu_name, string stu_seat)
        {
            return string.Format("{0}_{1}_{2}", stu_number, stu_name, stu_seat);
        }

        public static string GetStudentChartLocation(string folderName, string stu_number, string stu_name, string stu_seat)
        {
            string stufn = GetPath(folderName) + string.Format("/{0}", GetMainFN(stu_number, stu_name, stu_seat));
            return GetPath(stufn + "_student.png");
        }

        public static string GetClassChartLocation(string folderName, string stu_number, string stu_name, string stu_seat)
        {
            string stufn = GetPath(folderName) + string.Format("/{0}", GetMainFN(stu_number, stu_name, stu_seat));
            return GetPath(stufn + "_class.png");
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string xmlFile = txtXmlFile.Text;
            string pdf = txtPdf.Text;

            DisableButtons();
            Task task = Task.Factory.StartNew(() =>
            {
                MergeDocuments(xmlFile, pdf);
            });

            task.ContinueWith(x =>
            {
                if (x.Exception != null)
                    MessageBox.Show(x.Exception.InnerExceptions[0].Message);

                EnableButtons();
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void MergeDocuments(string xmlFile, string pdfFolderName)
        {
            XElement ds = XElement.Load(GetPath(xmlFile));

            int student_total = ds.XPathSelectElements("Class/Student").Count();
            int student_count = 0;
            Aspose.Words.Document doctemplate = new Aspose.Words.Document("template.docx");
            object sync = new object();

            Parallel.ForEach(ds.XPathSelectElements("Class/Student"), student =>
            {
                Aspose.Words.Document doc = null;
                lock (sync)
                {
                    doc = doctemplate.Clone();
                }

                HistorySource hs = new HistorySource(student, "", "Student");
                string id = student.Element("ID").Value;
                string stu_number = student.Element("StudentNumber").Value;
                string stu_name = student.Element("Name").Value;
                string stu_seat = student.Element("SeatNo").Value;

                doc.MailMerge.CleanupOptions = Aspose.Words.MailMerging.MailMergeCleanupOptions.RemoveEmptyParagraphs;
                doc.MailMerge.FieldMergingCallback = new FieldMergingCallback();
                doc.MailMerge.ExecuteWithRegions(hs);
                doc.MailMerge.DeleteFields();

                string fn = string.Format("{0}/{1}.pdf", GetPath(pdfFolderName), GetMainFN(stu_number, stu_name, stu_seat));
                Aspose.Words.Saving.SaveOptions so = Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Pdf);

                doc.Save(fn, Aspose.Words.SaveFormat.Pdf);
                //student_count++;
                Interlocked.Increment(ref student_count);
                ReportProgress(student_total, student_count);
            });

            //foreach (XElement student in ds.XPathSelectElements("Class/Student"))
            //{
            //    Aspose.Words.Document doc = new Aspose.Words.Document("template.docx");
            //    HistorySource hs = new HistorySource(student, "", "Student");
            //    string id = student.Element("ID").Value;
            //    string stu_number = student.Element("StudentNumber").Value;
            //    string stu_name = student.Element("Name").Value;
            //    string stu_seat = student.Element("SeatNo").Value;

            //    doc.MailMerge.CleanupOptions = Aspose.Words.MailMerging.MailMergeCleanupOptions.RemoveEmptyParagraphs;
            //    doc.MailMerge.FieldMergingCallback = new FieldMergingCallback();
            //    doc.MailMerge.ExecuteWithRegions(hs);
            //    doc.MailMerge.DeleteFields();

            //    string fn = string.Format("{0}/{1}.pdf", GetPath(pdfFolderName), GetMainFN(stu_number, stu_name, stu_seat));
            //    doc.Save(fn, Aspose.Words.SaveFormat.Pdf);
            //    student_count++;
            //    ReportProgress(student_total, student_count);
            //}

            Process.Start(pdfFolderName);
        }

        class FieldMergingCallback : IFieldMergingCallback
        {

            #region IFieldMergingCallback 成員

            public void FieldMerging(FieldMergingArgs args)
            {
            }

            public void ImageFieldMerging(ImageFieldMergingArgs args)
            {
                string filePath = args.FieldValue + "";

                if (string.IsNullOrWhiteSpace(filePath))
                    return;

                if (!File.Exists(filePath))
                    return;

                Bitmap img = new Bitmap(filePath);
                args.Image = img;
            }

            #endregion
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void GenerateChartsBackground()
        {
            DisableButtons();
            Task task = Task.Factory.StartNew(GenerateCharts);
            task.ContinueWith(x =>
            {
                if (x.Exception != null)
                    MessageBox.Show(x.Exception.InnerExceptions[0].Message);

                EnableButtons();
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void EnableButtons()
        {
            btnDraw.Enabled = true;
            btnRun.Enabled = true;
        }

        private void DisableButtons()
        {
            btnDraw.Enabled = false;
            btnRun.Enabled = false;
        }

        private void GenerateCharts()
        {
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook("example.xlsx");

            Aspose.Cells.Rendering.ImageOrPrintOptions opt = new Aspose.Cells.Rendering.ImageOrPrintOptions();
            opt.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
            opt.VerticalResolution = 300;
            opt.HorizontalResolution = 300;

            Worksheet ws = wb.Worksheets[0];

            XElement data = XElement.Load(GetPath(txtXmlFile.Text));
            ClassAvg avglookup = new ClassAvg(data);

            int student_total = data.XPathSelectElements("Class/Student").Count();
            int student_count = 0;

            foreach (XElement cls in data.XPathSelectElements("Class"))
            {
                string class_name = cls.Attribute("className").Value;
                wb.Worksheets.GetRangeByName("DataRange").PutValue("", false, false);

                Range clsRng = wb.Worksheets.GetRangeByName("Class");

                int clsOffset = clsRng.FirstRow;
                Dictionary<string, decimal> clslookup = avglookup.GetClassAvg(class_name);

                foreach (string groupName in BookGroup.Groups)
                {
                    if (clslookup.ContainsKey(groupName))
                        ws.Cells[clsOffset, clsRng.FirstColumn].PutValue(clslookup[groupName]);
                    else
                        ws.Cells[clsOffset, clsRng.FirstColumn].PutValue("");

                    clsOffset++;
                }
                //處理「未分類」。
                if (clslookup.ContainsKey(string.Empty))
                    ws.Cells[clsOffset, clsRng.FirstColumn].PutValue(clslookup[string.Empty]);
                else
                    ws.Cells[clsOffset, clsRng.FirstColumn].PutValue(string.Empty);

                foreach (XElement student in cls.XPathSelectElements("Student"))
                {
                    string stu_number = student.Element("StudentNumber").Value;
                    string stu_name = student.Element("Name").Value;
                    string stu_seat = student.Element("SeatNo").Value;
                    Dictionary<string, decimal> studentlookup = new Dictionary<string, decimal>();

                    foreach (XElement history in student.XPathSelectElements("Histories/History"))
                    {
                        string bookid = history.Attribute("BookGroupID").Value;
                        string type = BookGroup.ParseID(bookid);

                        if (!studentlookup.ContainsKey(type))
                            studentlookup[type] = 0;

                        studentlookup[type]++;
                    }

                    Range stuRng = wb.Worksheets.GetRangeByName("Personal");
                    int stuOffset = stuRng.FirstRow;

                    foreach (string groupName in BookGroup.Groups)
                    {
                        if (studentlookup.ContainsKey(groupName))
                            ws.Cells[stuOffset, stuRng.FirstColumn].PutValue(studentlookup[groupName]);
                        else
                            ws.Cells[stuOffset, stuRng.FirstColumn].PutValue("");

                        stuOffset++;
                    }
                    //處理「未分類」
                    if (studentlookup.ContainsKey(string.Empty))
                        ws.Cells[stuOffset, stuRng.FirstColumn].PutValue(studentlookup[string.Empty]);
                    else
                        ws.Cells[stuOffset, stuRng.FirstColumn].PutValue(string.Empty);

                    ws.Charts["PersonalChart"].ToImage(GetStudentChartLocation(txtChart.Text, stu_number, stu_name, stu_seat), opt);
                    ws.Charts["ClassChart"].ToImage(GetClassChartLocation(txtChart.Text, stu_number, stu_name, stu_seat), opt);
                    //wb.Save(GetStudentChartLocation(txtChart.Text, stu_number, stu_name, stu_seat) + ".xls");

                    student_count++;
                    ReportProgress(student_total, student_count);
                }
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            GenerateChartsBackground();
        }

        private void ReportProgress(int total, int current)
        {
            if (InvokeRequired)
                Invoke(new Action<int, int>(ReportProgress), new object[] { total, current });
            else
            {
                if (total == current)
                    lblProgress.Text = string.Format("產生完成：{0}", total);
                else
                    lblProgress.Text = string.Format("進度：{0}/{1}", current, total);
            }
        }
    }
}
