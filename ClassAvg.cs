using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ReadHistoryGenerator
{
    class ClassAvg
    {
        private Dictionary<string, Dictionary<string, decimal>> Classes = new Dictionary<string, Dictionary<string, decimal>>();

        public ClassAvg(XElement data)
        {
            foreach (XElement record in data.Elements("Class"))
            {
                Dictionary<string, decimal> avg = new Dictionary<string, decimal>();
                string class_name = record.Attribute("className").Value;
                Classes.Add(class_name, avg);

                var histories = record.XPathSelectElements("Student/Histories/History");
                avg["StudentCount"] = record.XPathSelectElements("Student").Count();
                foreach (XElement history in histories)
                {
                    string bookid = history.Attribute("BookGroupID").Value;
                    string type = BookGroup.ParseID(bookid);

                    if (!avg.ContainsKey(type))
                        avg[type] = 0;

                    avg[type]++;
                }
            }
        }

        public Dictionary<string, decimal> GetClassAvg(string className)
        {
            if (!Classes.ContainsKey(className))
                return new Dictionary<string, decimal>();

            return Classes[className];
        }
    }
}
