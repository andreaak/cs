using System.Collections.Generic;
using Utils.WorkWithFiles;

namespace Localization.Writers
{
    class PoWriter : IWriter
    {
        const string baseLinePattern = "msgid \"{0}\"";
        const string localizedLinePattern = "msgstr \"{0}\"";

        
        private string filePath;
        private Logger writter;

        public PoWriter(string filePath)
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
                writter.WriteLine(string.Empty);
                writter.WriteLine(string.Format(baseLinePattern, item.Key));
                writter.WriteLine(string.Format(localizedLinePattern, item.Value));
            }
            writter.Close();
        }

        #endregion
    }
}
