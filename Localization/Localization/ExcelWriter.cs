using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.WorkWithFiles;

namespace Localization
{
    class ExcelWriter : IWriter
    {
        private string filePath;
        private Logger writter;

        public ExcelWriter(string filePath)
        {
            this.filePath = filePath;
            writter = Logger.GetInstance();
        }

        #region IWriter Members

        public void Write(SortedDictionary<string, string> words)
        {
            writter.Init(filePath);
            foreach (var item in words)
            {
                writter.WriteLine(item.Key + Options.ExcelSeparator + item.Value);
            }
            writter.Close();
        }

        #endregion
    }
}
