using System;
using System.Text;
using SharpSvn;
using SharpSvn.Implementation;

namespace WorkWithSvn
{
    public class LogData 
    {
        private string author;

        public string Author
        {
            get { return author; }
        }
        private string revision;
        public string Revision
        {
            get { return revision; }
        }

        private SvnChangeItemCollection files;
        private string formatedFiles;
        public string Files
        {
            get { return formatedFiles; }
        }
        private string message;

        public string Message
        {
            get { return message; }
        }
        private DateTime date;

        public string Date
        {
            get { return date.ToString(); }
        }

        public LogData(string author, string revision, SvnChangeItemCollection files, string message, DateTime date)
        {
            this.author = author;
            this.revision = revision;
            this.files = files;
            this.message = message;
            this.date = date;
            FormatFiles(files);
        }

        private void FormatFiles(SvnChangeItemCollection files)
        {
            StringBuilder formatedFilesSb = new StringBuilder();
            foreach (SvnChangeItem item in files)
            {
                if (formatedFilesSb.Length > 0)
                {
                    formatedFilesSb.Append(Environment.NewLine);
                }
                formatedFilesSb.Append(new StringBuilder(item.Action.ToString()).Append(" ").Append(item.Path));
            }    
            formatedFiles = formatedFilesSb.ToString();
        }
    }
}
