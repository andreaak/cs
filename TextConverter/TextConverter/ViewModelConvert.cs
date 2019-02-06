using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static TextConverter.DownloadHelper;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    public class ViewModelConvert
    {
        public static string[] extensions = { ".txt", ".htm", ".html", ".css", ".js", ".cs", ".java" };
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
                    || !encName.Equals(SrcEncodingName, StringComparison.OrdinalIgnoreCase))
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

        // Words

        public async Task ConvertAsync(WordData data)
        {
            if (Directory.Exists(data.Path))
            {
                var files = Directory.EnumerateFiles(data.Path, "*" + data.Converter.InExtension, SearchOption.TopDirectoryOnly).ToArray();
                foreach (string filePath in files)
                {
                    await ConvertFileAsync(filePath, data);
                }
            }
            else
            {
                await ConvertFileAsync(data.Path, data);
            }
        }

        private async Task ConvertFileAsync(string filePath, WordData data)
        {
            string suffix = string.IsNullOrEmpty(data.Region) ? "" : $"_{data.Region}";
            string outputFilePath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar
                + "lesson_" + Path.GetFileNameWithoutExtension(filePath) + suffix + data.Converter.OutExtension;
            await data.Converter.ConvertFileAsync(filePath, outputFilePath, data);
        }

        //Verb

        public async Task RequestOutputAndConvertAsync(WordData data)
        {
            string outputFilePath = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };


            if (SelectFile.SaveFile("Save XML", "", ref outputFilePath, extensions))
            {
                await data.Converter.ConvertFileAsync(data.Path, outputFilePath, data);
            }
        }

        //Sound

        public Task DownloadSoundsAsync(string wordListFilePath, string soundFormat,
            Action<string> infoAction, Action<string> errorAction)
        {
            return Task.Factory.StartNew(() =>
            {
                var files = File.ReadAllLines(wordListFilePath);
                HashSet<string> notFinded = new HashSet<string>();
                using (var web = new WebClient())
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        var words = files[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            string normalized = GetNormalized(word);
                            if (notFinded.Contains(normalized))
                            {
                                continue;
                            }
                            bool res = DownloadHelper.DownloadWordsSounds(web, normalized, soundFormat, infoAction, errorAction);
                            if (!res)
                            {
                                notFinded.Add(normalized);
                            }
                        }
                    }
                }
            });
        }

        public Task DownloadWordsSoundsAsync(string wordListFilePath, string soundFormat,
                    Action<string> infoAction, Action<string> errorAction)
        {
            return Task.Factory.StartNew(() =>
            {
                var words = CSVHelper.ReadWords(wordListFilePath);

                using (var web = new WebClient())
                {
                    foreach (var word in words)
                    {
                        string normalized = GetNormalized(word.GetItem(En));
                        if (string.IsNullOrEmpty(normalized))
                        {
                            continue;
                        }
                        DownloadHelper.DownloadWordsSounds(web, normalized, soundFormat, infoAction, errorAction);
                    }
                }
            });
        }

        public Task DownloadVerbsSoundsAsync(string wordListFilePath, string soundFormat,
            Action<string> infoAction, Action<string> errorAction)
        {
            return Task.Factory.StartNew(() =>
            {
                var verbs = CSVHelper.ReadVerbs(wordListFilePath);

                using (var web = new WebClient())
                {
                    foreach (var verb in verbs)
                    {
                        DownloadHelper.DownloadVerbSounds(web, verb.Infinitive, soundFormat, infoAction, errorAction);
                        DownloadHelper.DownloadVerbSounds(web, verb.PastSimple, soundFormat, infoAction, errorAction);
                        DownloadHelper.DownloadVerbSounds(web, verb.PastPaticiple, soundFormat, infoAction, errorAction);
                    }
                }
            });
        }

    }

    class Brackets
    {
        public bool IsBrackets { get; set; }
    }
}
