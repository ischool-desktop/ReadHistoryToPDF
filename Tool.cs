using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;

namespace ReadHistoryGenerator
{
    public static class Util
    {
        public static string GenerateFileName(string ID)
        {
            return string.Format("2014101{0}01", ID);
        }
    }

    public class IDNumberLookup
    {
        private Dictionary<string, StudentData> Lookup = new Dictionary<string, StudentData>();

        public IDNumberLookup(string fileName)
        {
            Workbook wb = new Workbook(fileName);
            Worksheet ws = wb.Worksheets[0];

            Dictionary<string, int> header_lookup = ReadColumnHeaders(ws);

            int row_offset = 1;
            for (int i = 1; i <= ws.Cells.MaxDataRow; i++)
            {
                string grade = ws.Cells[row_offset, header_lookup["年級"]].StringValue;
                string cls = ws.Cells[row_offset, header_lookup["班級"]].StringValue;
                string cn = grade + "年" + GetChineseNum(cls) + "班";
                string sno = ws.Cells[row_offset, header_lookup["座號"]].StringValue;
                string idnum = ws.Cells[row_offset, header_lookup["統編"]].StringValue + ""; //身分證號
                string name = ws.Cells[row_offset, header_lookup["學生"]].StringValue; //姓名
                string stunum = ws.Cells[row_offset, header_lookup["學號"]].StringValue; //姓名

                string key = GetKey(cn, sno);

                if (!Lookup.ContainsKey(key))
                    Lookup.Add(key, new StudentData() { IDNumber = idnum, Name = name, StudentNumber = stunum });

                row_offset++;
            }
        }

        private string GetChineseNum(string cls)
        {
            switch (cls)
            {
                case "1":
                    return "一";
                case "2":
                    return "二";
                case "3":
                    return "三";
                case "4":
                    return "四";
                case "5":
                    return "五";
                case "6":
                    return "六";
                case "7":
                    return "七";
                case "8":
                    return "八";
                case "9":
                    return "九";
                case "10":
                    return "十";
                default:
                    return string.Empty;
            }
        }

        private string GetKey(string cn, string sno)
        {
            return string.Format("{0}#{1}", cn, sno);
        }

        private static Dictionary<string, int> ReadColumnHeaders(Worksheet ws)
        {
            Dictionary<string, int> header_lookup = new Dictionary<string, int>();
            for (int i = 0; i <= ws.Cells.MaxDataColumn; i++)
            {
                string header = ws.Cells[0, i].StringValue;
                if (!header_lookup.ContainsKey(header))
                    header_lookup.Add(header, i);
            }
            return header_lookup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cn">班級名稱，例：三年二班。</param>
        /// <param name="sno">座號(數字)。</param>
        /// <returns></returns>
        public StudentData GetStudentData(string cn, string sno)
        {
            string key = GetKey(cn, sno);

            if (Lookup.ContainsKey(key))
                return Lookup[key].Clone();
            else
                return null;
        }

        public class StudentData
        {
            public string Name { get; set; }

            public string IDNumber { get; set; }

            public string StudentNumber { get; set; }

            public StudentData Clone()
            {
                StudentData data = new StudentData();
                data.Name = this.Name;
                data.IDNumber = this.IDNumber;
                data.StudentNumber = this.StudentNumber;

                return data;
            }
        }
    }
}