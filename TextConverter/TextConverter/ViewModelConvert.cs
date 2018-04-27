using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TextConverter.Models;
using Utils;

namespace TextConverter
{
    public class ViewModelConvert
    {
        public static string[] extensions = { ".txt", ".htm", ".html", ".css", ".js", ".cs", ".java" };
        public static readonly string[] separators = new string[] { "(", ")", "/", "-" };
        public event PropertyChangedEventHandler PropertyChanged;
        private const string OutputWordsSounds = "Sounds/{1}/{0}_{1}.mp3";
        private const string OutputVerbsSounds = "Sounds/Irregular/{1}/{0}_{1}.mp3";

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

        public void ConvertWordToXml(string path)
        {
            if (Directory.Exists(path))
            {
                ConvertWordsFromCSVToXML(path);
            }
            else
            {
                string outputFilePath = "";
                string[] extensions = new string[]
                {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
                };

                if (SelectFile.SaveFile("Save XML", "", ref outputFilePath, extensions))
                {
                    var words = CSVHelper.ReadWords(path);
                    DownloadWordsTranscription(words, null, CSVHelper.Uk);
                    XMLWriteHelper.WriteWords(outputFilePath, words);
                }
            }
        }

        public void ConvertWordToXmlAndDownloadTranscription(string path, Action<string> errorAction, string region)
        {
            if (Directory.Exists(path))
            {
                ConvertWordsFromCSVToXML(path, region, (words) => DownloadWordsTranscriptionForce(words, errorAction, region));
            }
            else
            {
                ConvertWordFromCSVToXML(path, region, (words) => DownloadWordsTranscriptionForce(words, errorAction, region));
            }
        }

        private void DownloadWordsTranscriptionForce(IList<WordItem> words, Action<string> errorAction, string region)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var word in words)
                {
                    string normalized = GetNormalized(word.GetItem(CSVHelper.En));
                    string trExisted = GetNormalized(word.GetItem(CSVHelper.EnTr));

                    string tr;
                    if (IsMultiWord(normalized))
                    {
                        tr = DownloadMultiWordsTranscription(errorAction, web, normalized, region);
                    }
                    else
                    {
                        tr = DownloadTranscription(web, normalized, ParseTranscription, errorAction, region);
                    }
                    if (!string.IsNullOrEmpty(tr) && trExisted != tr)
                    {
                        word.AddItem(CSVHelper.EnTr, tr);
                    }
                }
            }
        }

        private void DownloadWordsTranscription(IList<WordItem> words, Action<string> errorAction, string region)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var word in words)
                {
                    string normalized = GetNormalized(word.GetItem(CSVHelper.En));
                    string trExisted = GetNormalized(word.GetItem(CSVHelper.EnTr));
                    if (!string.IsNullOrEmpty(trExisted))
                    {
                        continue;
                    }
                    string tr;
                    if (IsMultiWord(normalized))
                    {
                        tr = DownloadMultiWordsTranscription(errorAction, web, normalized, region);
                    }
                    else
                    {
                        tr = DownloadTranscription(web, normalized, ParseTranscription, errorAction, region);
                    }
                    if (!string.IsNullOrEmpty(tr))
                    {
                        word.AddItem(CSVHelper.EnTr, tr);
                    }
                }
            }
        }

        private bool IsMultiWord(string normalized)
        {
            return normalized.Contains(" ") || normalized.Contains("-");
        }

        private string DownloadMultiWordsTranscription(Action<string> errorAction, WebClient web, string normalized, string region)
        {
            var words = normalized.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            bool isBrackets = false;
            foreach (var word in words)
            {
                ProcessWord(web, sb, GetNormalized(word), ref isBrackets, errorAction, region);
            }
            sb.Insert(0, "[");
            sb.Append("]");
            return sb.ToString();
        }

        private void ProcessWord(WebClient web, StringBuilder sb, string wrd, ref bool isBrackets,
            Action<string> errorAction, string region)
        {
            if (wrd.StartsWith("("))
            {
                isBrackets = true;
            }
            if (isBrackets)
            {
                if (wrd.EndsWith(")"))
                {
                    isBrackets = false;
                }
                return;
            }
            if (sb.Length != 0)
            {
                sb.Append(" ");
            }
            if (separators.Contains(wrd))
            {
                sb.Append(wrd);
                return;
            }
            string tr = DownloadTranscription(web, wrd, ParseTranscriptionWithoutBrackets, errorAction, region);
            if (!string.IsNullOrEmpty(tr))
            {
                sb.Append(tr);
            }
            else
            {
                if (wrd.Contains("-"))
                {
                    var words = wrd.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        ProcessWord(web, sb, GetNormalized(word), ref isBrackets, errorAction, region);
                    }
                }
                else
                {
                    sb.Append($"<{wrd}>");
                }
            }
        }

        private void ConvertWordsFromCSVToXML(string directory, string region = null, Action<IList<WordItem>> action = null)
        {
            foreach (string filePath in Directory.EnumerateFiles(directory, "*.csv", SearchOption.TopDirectoryOnly))
            {
                ConvertWordFromCSVToXML(filePath, region, action);
            }
            return;
        }

        private void ConvertWordFromCSVToXML(string filePath, string region, Action<IList<WordItem>> action)
        {
            var words = CSVHelper.ReadWords(filePath);
            action?.Invoke(words);
            string suffix = string.IsNullOrEmpty(region) ? "" : $"_{region}";
            XMLWriteHelper.WriteWords(Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar
                + "lesson_" + Path.GetFileNameWithoutExtension(filePath) + suffix + ".xml", words);
        }

        //Verb

        public void ConvertVerbToXMLAndDownloadVerbTranscription(string path, Action<string> errorAction, string region)
        {
            string outputFilePath = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };

            if (SelectFile.SaveFile("Save XML", "", ref outputFilePath, extensions))
            {
                var verbs = CSVHelper.ReadVerbs(path);
                DownloadTranscriptions(verbs, errorAction, region);
                XMLWriteHelper.WriteVerbs(outputFilePath, verbs);
                CSVHelper.WriteVerbs(outputFilePath + ".csv", verbs);
            }
        }

        private void DownloadTranscriptions(IList<EnVerbItem> verbs, Action<string> errorAction, string region)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var verb in verbs)
                {
                    //if (string.IsNullOrEmpty(verb.InfinitiveTranscription))
                    {
                        //var temps = GetWords(verb.Infinitive);
                        verb.InfinitiveTranscription = DownloadMultiVerbTranscription(web, verb.Infinitive, errorAction, region);
                    }
                    //if (string.IsNullOrEmpty(verb.PastSimpleTranscription))
                    {
                        //var temps = GetWords(verb.PastSimple);
                        verb.PastSimpleTranscription = DownloadMultiVerbTranscription(web, verb.PastSimple, errorAction, region);
                    }
                    //if (string.IsNullOrEmpty(verb.PastPaticipleTranscription))
                    {
                        //var temps = GetWords(verb.PastPaticiple);
                        verb.PastPaticipleTranscription = DownloadMultiVerbTranscription(web, verb.PastPaticiple, errorAction, region);
                    }
                }
            }
        }

        private string DownloadMultiVerbTranscription(WebClient web, string verbs, Action<string> errorAction, string region)
        {
            string normalized = GetNormalized(verbs);
            string tr;
            if (IsMultiWord(normalized))
            {
                tr = DownloadMultiWordsTranscription(errorAction, web, normalized, region);
            }
            else
            {
                tr = DownloadTranscription(web, normalized, ParseTranscription, errorAction, region);
            }
            return tr;
        }

        private string GetNormalized(string word)
        {
            return word.Trim().ToLower();
        }

        //Plain List Transcription

        public void CombineRuEnAndDownloadPlainListTranscriptions(string path, Action<string> errorAction, string region)
        {
            string outputFilePath = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };

            if (SelectFile.SaveFile("Save XML", "", ref outputFilePath, extensions))
            {
                var words = File.ReadAllLines(path);

                List<WordItem> wordItems = new List<WordItem>();
                using (var web = new WebClient())
                {
                    web.Encoding = Encoding.UTF8;

                    for (int i = 0; i < words.Length - 1;)
                    {
                        string en = words[i++];
                        string ru = words[i++];
                        string normalized = GetNormalized(en);

                        string tr;
                        if (IsMultiWord(normalized))
                        {
                            tr = DownloadMultiWordsTranscription(errorAction, web, normalized, region);
                        }
                        else
                        {
                            tr = DownloadTranscription(web, normalized, ParseTranscription, errorAction, region);
                        }

                        var item = new WordItem();
                        item.AddItem(CSVHelper.Ru, ru);
                        item.AddItem(CSVHelper.En, en);
                        if ()
                            item.AddItem(CSVHelper.EnTr, tr);
                        wordItems.Add(item);
                    }
                }

                XMLWriteHelper.WriteWords(outputFilePath, wordItems);
            }
        }

        // Other

        private string DownloadTranscription(WebClient web, string normalized,
            Func<string, string, string, string> parseAction, Action<string> errorAction, string region)
        {
            string tr = null;
            try
            {
                string res = string.Format("http://wooordhunt.ru/word/{0}", normalized);
                var html = web.DownloadString(res);
                tr = parseAction(html, normalized, region);
            }
            catch (Exception ex)
            {
                errorAction?.Invoke(ex.Message);
            }
            return tr;
        }

        private string ParseTranscription(string html, string normalized, string region)
        {
            string transcription = ParseTranscriptionWithoutBrackets(html, normalized, region);
            if (string.IsNullOrEmpty(transcription))
            {
                return string.Empty;
            }
            return "[" + transcription + "]";
        }

        private string ParseTranscriptionWithoutBrackets(string html, string normalized, string region)
        {
            const string usPattern = "<span title=\"американская транскрипция слова {0}\" class=\"transcription\">";
            const string ukPattern = "<span title=\"британская транскрипция слова {0}\" class=\"transcription\">";
            string startPattern = region == "us" ? usPattern : ukPattern;
            string search = string.Format(startPattern, normalized);
            int startIndex = html.IndexOf(search, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return string.Empty;
            }
            startIndex += search.Length;

            string endPattern = "</span>";
            int endIndex = html.IndexOf(endPattern, startIndex, StringComparison.OrdinalIgnoreCase);
            string output = html.Substring(startIndex, endIndex - startIndex).Replace("|", "").Trim();
            return output;
        }

        //Sound

        public Task DownloadSoundsAsync(string wordListFilePath,
            Action<string> infoAction, Action<string> errorAction)
        {

            return Task.Factory.StartNew(() =>
            {
                var files = File.ReadAllLines(wordListFilePath);

                using (var web = new WebClient())
                {
                    for (int i = 0; i < files.Length; i += 2)
                    {
                        string normalized = GetNormalized(files[i]);

                        DownloadSounds(web, normalized, infoAction, errorAction, OutputWordsSounds);
                    }
                }
            });
        }

        public Task DownloadWordsSoundsAsync(string wordListFilePath,
                    Action<string> infoAction, Action<string> errorAction)
        {

            return Task.Factory.StartNew(() =>
            {
                var words = CSVHelper.ReadWords(wordListFilePath);

                using (var web = new WebClient())
                {
                    foreach (var word in words)
                    {
                        string normalized = GetNormalized(word.GetItem(CSVHelper.En));

                        DownloadSounds(web, normalized, infoAction, errorAction, OutputWordsSounds);
                    }
                }
            });
        }

        public Task DownloadVerbsSoundsAsync(string wordListFilePath,
            Action<string> infoAction, Action<string> errorAction)
        {

            return Task.Factory.StartNew(() =>
            {
                var verbs = CSVHelper.ReadVerbs(wordListFilePath);

                using (var web = new WebClient())
                {
                    foreach (var verb in verbs)
                    {
                        DownloadVerbSounds(web, verb.Infinitive, infoAction, errorAction);
                        DownloadVerbSounds(web, verb.PastSimple, infoAction, errorAction);
                        DownloadVerbSounds(web, verb.PastPaticiple, infoAction, errorAction);
                    }
                }
            });
        }

        private void DownloadVerbSounds(WebClient web, string verbsCombined, Action<string> infoAction, Action<string> errorAction)
        {
            var verbs = verbsCombined.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var verb in verbs)
            {
                DownloadSounds(web, GetNormalized(verb), infoAction, errorAction, OutputVerbsSounds);
            }
        }


        private void DownloadSounds(WebClient web, string normalized, Action<string> infoAction, Action<string> errorAction, string destination)
        {
            try
            {
                infoAction(normalized);
                if (!IsWordFileExist(normalized, CSVHelper.Us, destination))
                {
                    DownloadSound(web, normalized, CSVHelper.Us, destination);
                }
                if (!IsWordFileExist(normalized, CSVHelper.Uk, destination))
                {
                    DownloadSound(web, normalized, CSVHelper.Uk, destination);
                }
            }
            catch (Exception ex)
            {
                errorAction(ex.Message);
            }
        }

        private bool DownloadSound(WebClient web, string normalized, string region, string destination)
        {
            string res = string.Format("http://wooordhunt.ru/data/sound/word/{0}/mp3/{1}.mp3",
                                region, normalized);
            var bt = web.DownloadData(res);
            if (IsError(bt)) //html page
            {
                return false;
            }


            string dest = string.Format(destination, normalized, region);
            string folder = Path.GetDirectoryName(dest);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllBytes(dest, bt);
            return true;
        }

        private bool IsWordFileExist(string normalized, string region, string destination)
        {
            return File.Exists(string.Format(destination, normalized, region));
        }

        private bool IsError(byte[] bt)
        {
            return bt.Length < 4 ||
                   (bt[0] == 60 && bt[1] == 33 && bt[2] == 68 && bt[3] == 79);//html page
        }

    }
}
