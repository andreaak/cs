using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace TextConverter
{
    public class ViewModelConvert
    {
        public static string[] extensions = { ".txt", ".htm", ".html", ".css", ".js", "*.cs", "*.java" };
        public event PropertyChangedEventHandler PropertyChanged;

        public EncodingInfo[] Encodings
        {
            get
            {
                return Encoding.GetEncodings();
            }
        }

        private string srcEncodingName;
        public string SrcEncodingName
        {
            get
            {
                return srcEncodingName;
            }
            set
            {
                if (srcEncodingName != value)
                {
                    srcEncodingName = value;
                    NotifyPropertyChanged("SrcEncodingName");
                }
            }
        }

        private string dstEncodingName;
        public string DstEncodingName
        {
            get
            {
                return dstEncodingName;
            }
            set
            {
                if (dstEncodingName != value)
                {
                    dstEncodingName = value;
                    NotifyPropertyChanged("DstEncodingName");
                }
            }
        }

        public void Convert(string folderPath)
        {
            Encoding srcEnc = Encoding.GetEncoding(SrcEncodingName);
            Encoding destEnc = Encoding.GetEncoding(DstEncodingName);
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            Convert(dir, srcEnc, destEnc);
        }

        public void Convert(DirectoryInfo dir, Encoding srcEnc, Encoding destEnc)
        {
            foreach (FileInfo file in dir.GetFiles().Where(file => extensions.Contains(file.Extension)))
            {
                string encName = GetCharset(file.FullName);

                if (string.IsNullOrEmpty(encName) 
                    || encName.Equals(DstEncodingName, StringComparison.OrdinalIgnoreCase)
                    || encName != SrcEncodingName)
                {
                    continue;
                }
                Decode(file, srcEnc, destEnc);
            }

            foreach (DirectoryInfo innerDir in dir.GetDirectories())
            {
                Convert(innerDir, srcEnc, destEnc);
            }
        }

        private void Decode(FileInfo file, Encoding srcEnc, Encoding dstEnc)
        {
            byte[] srcBytes = File.ReadAllBytes(file.FullName);
            byte[] resBytes = Encoding.Convert(srcEnc, dstEnc, srcBytes);
            File.WriteAllBytes(file.FullName, resBytes);
        }

        public static string GetCharset(string filename)
        {
            string res = null;
            using (FileStream fs = File.OpenRead(filename))
            {
                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                if (cdet.Charset != null)
                {
                    res = cdet.Charset;
                }
            }
            return res;
        } 

        #region INotifyPropertyChanged Members

        /// Need to implement this interface in order to get data binding
        /// to work properly.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


    }
}
