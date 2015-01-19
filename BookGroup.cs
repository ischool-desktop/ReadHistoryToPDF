using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadHistoryGenerator
{
    public class BookGroup
    {
        private static List<BookGroup> GroupList = new List<BookGroup>();

        static BookGroup()
        {
            GroupList.Add(new BookGroup(900, "藝術類"));
            GroupList.Add(new BookGroup(800, " 語文類"));
            GroupList.Add(new BookGroup(700, "世界史地"));
            GroupList.Add(new BookGroup(600, "中國史地"));
            GroupList.Add(new BookGroup(500, "社會科學類"));
            GroupList.Add(new BookGroup(400, "應用科學類"));
            GroupList.Add(new BookGroup(300, "科學類"));
            GroupList.Add(new BookGroup(200, "宗教類"));
            GroupList.Add(new BookGroup(100, "哲學類"));
            GroupList.Add(new BookGroup(0, "總類"));
        }

        public static List<string> Groups
        {
            get
            {
                List<string> gs = GroupList.ConvertAll(x => x.Letter);
                gs.Reverse();

                return gs;
            }
        }

        /// <summary>
        /// 將 BookGroupID 格式轉換成中文。
        /// </summary>
        /// <param name="bookGrouopId"></param>
        /// <returns></returns>
        public static string ParseID(string bookGrouopId)
        {
            if (string.IsNullOrWhiteSpace(bookGrouopId))
                return string.Empty;

            string[] pair = bookGrouopId.Split('.');

            decimal typeCode;
            if (decimal.TryParse(pair[0], out typeCode))
            {
                return BookGroup.Convert(typeCode).Letter;
            }
            else
                return string.Empty;
        }

        public static BookGroup Convert(decimal code)
        {
            foreach (BookGroup gpa in GroupList)
            {
                if (code >= gpa.Limit)
                    return gpa;
            }

            return GroupList[GroupList.Count - 1]; //最低那個。
        }

        public BookGroup(decimal limit, string letter)
        {
            Limit = limit;
            Letter = letter;
        }

        public decimal Limit { get; private set; }

        public string Letter { get; private set; }

        public override string ToString()
        {
            return string.Format("Letter:{0}, Limit:{1}", Letter, Limit);
        }
    }
}
