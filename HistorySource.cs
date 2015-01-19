using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ReadHistoryGenerator
{
    class HistorySource : Aspose.Words.MailMerging.IMailMergeDataSource
    {
        #region IMailMergeDataSource 成員

        private string _table_name;

        private List<XElement> _records;
        private int index = -1;
        private XElement _current;
        private Dictionary<string, Func<string, string>> Formators = new Dictionary<string, Func<string, string>>();

        public HistorySource(XElement data, string path, string name)
        {
            _table_name = name;
            _records = new List<XElement>();

            if (string.IsNullOrWhiteSpace(path))
            {
                _records.Add(data);
            }
            else
            {
                foreach (XElement record in data.XPathSelectElements(path))
                    _records.Add(record);
            }
        }

        public void AddFormator(string name, Func<string, string> func)
        {
            Formators.Add(name, func);
        }

        public Aspose.Words.MailMerging.IMailMergeDataSource GetChildDataSource(string tableName)
        {
            HistorySource hs = new HistorySource(_current, "Histories/History", "Book");

            hs.AddFormator("ReadDate", x =>
            {
                if (x.Length > 8)
                {
                    return x.Substring(0, 8);
                }
                else
                    return x;
            });
            hs.AddFormator("BookGroupID", x =>
            {
                return BookGroup.ParseID(x);
            });

            return hs;
        }

        public bool GetValue(string fieldName, out object fieldValue)
        {
            fieldValue = null;

            if (fieldName.StartsWith("Index", StringComparison.CurrentCultureIgnoreCase))
            {
                fieldValue = index + 1;
                return true;
            }

            string chart_path = "charts";

            if (fieldName.StartsWith("ChartStudent", StringComparison.CurrentCultureIgnoreCase))
            {
                string stu_number = _current.Element("StudentNumber").Value;
                string stu_name = _current.Element("Name").Value;
                string stu_seat = _current.Element("SeatNo").Value;

                string ffn = MainForm.GetStudentChartLocation(chart_path, stu_number, stu_name, stu_seat);
                fieldValue = ffn;
                return true;
            }

            if (fieldName.StartsWith("ChartClass", StringComparison.CurrentCultureIgnoreCase))
            {
                string stu_number = _current.Element("StudentNumber").Value;
                string stu_name = _current.Element("Name").Value;
                string stu_seat = _current.Element("SeatNo").Value;

                string ffn = MainForm.GetClassChartLocation(chart_path, stu_number, stu_name, stu_seat);
                fieldValue = ffn;
                return true;
            }

            XElement v = _current.Element(fieldName);

            if (v != null)
                fieldValue = v.Value;
            else
            {
                XAttribute x = _current.Attribute(fieldName);
                if (x != null)
                    fieldValue = x.Value;
            }

            if (fieldValue != null)
            {
                if (Formators.ContainsKey(fieldName))
                {
                    fieldValue = (Formators[fieldName])(fieldValue + "");
                }
            }

            return fieldValue != null;
        }

        public bool MoveNext()
        {
            if (++index >= _records.Count)
                return false;

            _current = _records[index];

            return true;
        }

        public string TableName
        {
            get { return _table_name; }
        }

        #endregion
    }

}
