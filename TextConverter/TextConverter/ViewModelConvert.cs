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
using static TextConverter.DownloadHelper;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    public enum DownloadStatus
    {
        Force,
        OnlyNotExist
    }

    public class ViewModelConvert
    {
        public static string[] extensions = { ".txt", ".htm", ".html", ".css", ".js", ".cs", ".java" };
        public static readonly string[] separators = new string[] { "(", ")", "/", "-" };
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

        // Words CSV -> XML

        public async Task ConvertFromCSVToXmlAsync(string path, Action<string> infoAction, Action<string> errorAction)
        {
            if (Directory.Exists(path))
            {
                await ConvertFilesCSVToXMLAsync(path, null, DownloadStatus.OnlyNotExist, infoAction, errorAction);
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
                    await ConvertFileCSVToXMLAsync(path, outputFilePath, null, DownloadStatus.OnlyNotExist, infoAction, errorAction);
                }
            }
        }

        public async Task ConvertFromCSVToXmlAndDownloadTranscriptionAsync(string path, string region,
            Action<string> infoAction, Action<string> errorAction)
        {
            if (Directory.Exists(path))
            {
                await ConvertFilesCSVToXMLAsync(path, region, DownloadStatus.Force, infoAction, errorAction);
            }
            else
            {
                await ConvertFileCSVToXMLAsync(path, region, DownloadStatus.Force, infoAction, errorAction);
            }
        }

        private async Task ConvertFilesCSVToXMLAsync(string directory, string region, DownloadStatus isLoadTranscription,
            Action<string> infoAction, Action<string> errorAction)
        {
            var files = Directory.EnumerateFiles(directory, "*.csv", SearchOption.TopDirectoryOnly).ToArray();
            foreach (string filePath in files)
            {
                await ConvertFileCSVToXMLAsync(filePath, region, isLoadTranscription, infoAction, errorAction);
            }
            return;
        }

        private async Task ConvertFileCSVToXMLAsync(string filePath, string region, DownloadStatus isLoadTranscription,
           Action<string> infoAction, Action<string> errorAction)
        {
            string suffix = string.IsNullOrEmpty(region) ? "" : $"_{region}";
            string outputFilePath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar
                + "lesson_" + Path.GetFileNameWithoutExtension(filePath) + suffix + ".xml";
            await ConvertFileCSVToXMLAsync(filePath, outputFilePath, region, isLoadTranscription, infoAction, errorAction);
        }

        private async Task ConvertFileCSVToXMLAsync(string filePath, string outputFilePath, string region, DownloadStatus isLoadTranscription,
            Action<string> infoAction, Action<string> errorAction)
        {
            var words = CSVHelper.ReadWords(filePath);
            if (isLoadTranscription == DownloadStatus.Force)
            {
                await DownloadWordsTranscriptionForceAsync(words, region, infoAction, errorAction);
            }
            else if (isLoadTranscription == DownloadStatus.OnlyNotExist)
            {
                await DownloadWordsTranscriptionAsync(words, region, infoAction, errorAction);
            }

            XMLHelper.WriteWords(outputFilePath, words);
            CSVHelper.WriteWords(outputFilePath + ".csv", words);
        }

        private async Task DownloadWordsTranscriptionForceAsync(IList<WordItem> words, string region,
            Action<string> infoAction, Action<string> errorAction)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var word in words)
                {
                    string normalized = GetNormalized(word.GetItem(En));
                    infoAction?.Invoke(normalized);
                    string trExisted = GetNormalized(word.GetItem(EnTr));

                    bool isMultiWord = IsMultiWord(normalized);
                    string html = null;
                    if (string.IsNullOrEmpty(word.RankFile) && !isMultiWord)
                    {
                        html = await DownloadHtmlAsync(web, normalized, errorAction);
                        word.Rank = ParseRank(html, normalized);
                    }
                    string tr;
                    if (isMultiWord)
                    {
                        tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, region, errorAction);
                    }
                    else
                    {
                        html = (html ?? await DownloadHtmlAsync(web, normalized, errorAction));
                        tr = ParseTranscription(html, normalized, region);
                    }
                    if (!string.IsNullOrEmpty(tr) && trExisted != tr)
                    {
                        word.AddItem(EnTr, tr);
                    }
                }
            }
        }

        private async Task DownloadWordsTranscriptionAsync(IList<WordItem> words, string region, Action<string> infoAction, Action<string> errorAction)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var word in words)
                {
                    string normalized = GetNormalized(word.GetItem(En));
                    infoAction?.Invoke(normalized);

                    bool isMultiWord = IsMultiWord(normalized);
                    string html = null;
                    if (string.IsNullOrEmpty(word.RankFile) && !isMultiWord)
                    {
                        html = await DownloadHtmlAsync(web, normalized, errorAction);
                        word.Rank = ParseRank(html, normalized);
                    }

                    string trExisted = GetNormalized(word.GetItem(EnTr));
                    if (!string.IsNullOrEmpty(trExisted))
                    {
                        continue;
                    }
                    string tr;
                    if (isMultiWord)
                    {
                        tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, region, errorAction);
                    }
                    else
                    {
                        html = (html ?? await DownloadHtmlAsync(web, normalized, errorAction));
                        tr = ParseTranscription(html, normalized, region);
                    }
                    if (!string.IsNullOrEmpty(tr))
                    {
                        word.AddItem(EnTr, tr);
                    }
                }
            }
        }

        private async Task<string> DownloadMultiWordsTranscriptionAsync(WebClient web, string normalized, string region, Action<string> errorAction)
        {
            var words = normalized.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            var brackets = new Brackets();
            foreach (var word in words)
            {
                await ProcessWordAsync(web, sb, GetNormalized(word), brackets, region, errorAction);
            }
            sb.Insert(0, "[");
            sb.Append("]");
            return sb.ToString();
        }

        private bool IsMultiWord(string normalized)
        {
            return normalized.Contains(" ") || normalized.Contains("-");
        }

        private async Task ProcessWordAsync(WebClient web, StringBuilder sb, string wrd, Brackets brackets, string region, Action<string> errorAction)
        {
            if (wrd.StartsWith("("))
            {
                brackets.IsBrackets = true;
            }
            if (brackets.IsBrackets)
            {
                if (wrd.EndsWith(")"))
                {
                    brackets.IsBrackets = false;
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
            string tr = await DownloadTranscriptionAsync(web, wrd, ParseTranscriptionWithoutBrackets, errorAction, region);
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
                        await ProcessWordAsync(web, sb, GetNormalized(word), brackets, region, errorAction);
                    }
                }
                else
                {
                    sb.Append($"<{wrd}>");
                }
            }
        }

        //Verb

        public async Task ConvertVerbToXMLAndDownloadVerbTranscriptionAsync(string path, string region,
            Action<string> infoAction, Action<string> errorAction)
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
                await DownloadTranscriptionsAsync(verbs, region, infoAction, errorAction);
                XMLHelper.WriteVerbs(outputFilePath, verbs);
                CSVHelper.WriteVerbs(outputFilePath + ".csv", verbs);
            }
        }

        private async Task DownloadTranscriptionsAsync(IList<EnVerbItem> verbs, string region,
            Action<string> infoAction, Action<string> errorAction)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var verb in verbs)
                {
                    //if (string.IsNullOrEmpty(verb.InfinitiveTranscription))
                    {
                        //var temps = GetWords(verb.Infinitive);
                        infoAction?.Invoke(verb.Infinitive);
                        verb.InfinitiveTranscription = await DownloadMultiVerbTranscriptionAsync(web, verb.Infinitive, region, errorAction);
                    }
                    //if (string.IsNullOrEmpty(verb.PastSimpleTranscription))
                    {
                        //var temps = GetWords(verb.PastSimple);
                        infoAction?.Invoke(verb.PastSimple);
                        verb.PastSimpleTranscription = await DownloadMultiVerbTranscriptionAsync(web, verb.PastSimple, region, errorAction);
                    }
                    //if (string.IsNullOrEmpty(verb.PastPaticipleTranscription))
                    {
                        //var temps = GetWords(verb.PastPaticiple);
                        infoAction?.Invoke(verb.PastPaticiple);
                        verb.PastPaticipleTranscription = await DownloadMultiVerbTranscriptionAsync(web, verb.PastPaticiple, region, errorAction);
                    }
                }
            }
        }

        private async Task<string> DownloadMultiVerbTranscriptionAsync(WebClient web, string verbs, string region, Action<string> errorAction)
        {
            string normalized = GetNormalized(verbs);
            string tr;
            if (IsMultiWord(normalized))
            {
                tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, region, errorAction);
            }
            else
            {
                tr = await DownloadTranscriptionAsync(web, normalized, ParseTranscription, errorAction, region);
            }
            return tr;
        }



        //Plain List Transcription

        public async Task CombineRuEnAndDownloadPlainListTranscriptionsAsync(string path, string region,
            Action<string> infoAction, Action<string> errorAction)
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
                        infoAction?.Invoke(normalized);
                        string tr;
                        if (IsMultiWord(normalized))
                        {
                            tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, region, errorAction);
                        }
                        else
                        {
                            tr = await DownloadTranscriptionAsync(web, normalized, ParseTranscription, errorAction, region);
                        }

                        var item = new WordItem();
                        item.AddItem(Ru, ru);
                        item.AddItem(En, en);
                        if (!string.IsNullOrEmpty(tr))
                        {
                            item.AddItem(EnTr, tr);
                        }
                        wordItems.Add(item);
                    }
                }

                XMLHelper.WriteWords(outputFilePath, wordItems);
            }
        }

        public async Task DownloadPopularityListAsync(string path, string region, Action<string> infoAction, Action<string> errorAction)
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

                    for (int i = 0; i < words.Length; i++)
                    {
                        var word = words[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        string normalized = GetNormalized(word[0]);
                        int popularity = int.Parse(word[1]);

                        infoAction?.Invoke(normalized);

                        string html = await DownloadHtmlAsync(web, normalized, errorAction);
                        string tr = ParseTranscription(html, normalized, region);
                        string ru = ParseRu(html, normalized, region);

                        var item = new WordItem();
                        item.Rank = popularity;
                        item.AddItem(Ru, ru);
                        item.AddItem(En, normalized);
                        if (!string.IsNullOrEmpty(tr))
                        {
                            item.AddItem(EnTr, tr);
                        }
                        wordItems.Add(item);
                    }
                }

                for (int i = 0; i < 26; i++)
                {
                    char ch = (char)((int)'a' + i);
                    var items = wordItems.Where(item => item.GetItem(En).StartsWith(ch.ToString())).ToArray();
                    string filePath = Path.GetDirectoryName(outputFilePath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(outputFilePath) + "_" + ch + ".xml";
                    XMLHelper.WriteWords(filePath, items);
                }
                
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
                    for (int i = 0; i < files.Length; i += 1)
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

        // Words XML -> CSV

        public async Task ConvertFromXMLToCSVAsync(string path, Action<string> infoAction, Action<string> errorAction)
        {
            if (Directory.Exists(path))
            {
                await ConvertFilesXMLToCSVAsync(path, null, DownloadStatus.OnlyNotExist, infoAction, errorAction);
            }
            else
            {
                string outputFilePath = "";
                string[] extensions = new string[]
                {
                "XML Files (*.csv)|*.csv|",
                "All Files (*.*)|*.*"
                };

                if (SelectFile.SaveFile("Save csv", "", ref outputFilePath, extensions))
                {
                    await ConvertFileXMLToCSVAsync(path, outputFilePath, null, DownloadStatus.OnlyNotExist, infoAction, errorAction);
                }
            }
        }

        public async Task ConvertFromXMLToCsvAndDownloadTranscriptionAsync(string path, string region,
            Action<string> infoAction, Action<string> errorAction)
        {
            if (Directory.Exists(path))
            {
                await ConvertFilesXMLToCSVAsync(path, region, DownloadStatus.Force, infoAction, errorAction);
            }
            else
            {
                await ConvertFileXMLToCSVAsync(path, region, DownloadStatus.Force, infoAction, errorAction);
            }
        }

        private async Task ConvertFilesXMLToCSVAsync(string directory, string region, DownloadStatus isLoadTranscription,
                            Action<string> infoAction, Action<string> errorAction)
        {
            var files = Directory.EnumerateFiles(directory, "*.xml", SearchOption.TopDirectoryOnly).ToArray();
            foreach (string filePath in files)
            {
                await ConvertFileXMLToCSVAsync(filePath, region, isLoadTranscription, infoAction, errorAction);
            }
            return;
        }

        private async Task ConvertFileXMLToCSVAsync(string filePath, string region, DownloadStatus isLoadTranscription,
            Action<string> infoAction, Action<string> errorAction)
        {
            string suffix = string.IsNullOrEmpty(region) ? "" : $"_{region}";
            string outputFilePath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar
                + "lesson_" + Path.GetFileNameWithoutExtension(filePath) + suffix + ".csv";
            await ConvertFileXMLToCSVAsync(filePath, outputFilePath, region, isLoadTranscription, infoAction, errorAction);
        }

        private async Task ConvertFileXMLToCSVAsync(string filePath, string outputFilePath, string region, DownloadStatus isLoadTranscription,
            Action<string> infoAction, Action<string> errorAction)
        {
            var words = XMLHelper.ReadWords(filePath);
            if (isLoadTranscription == DownloadStatus.Force)
            {
                await DownloadWordsTranscriptionForceAsync(words, region, infoAction, errorAction);
            }
            else if (isLoadTranscription == DownloadStatus.OnlyNotExist)
            {
                await DownloadWordsTranscriptionAsync(words, region, infoAction, errorAction);
            }
            CSVHelper.WriteWords(outputFilePath, words);
            XMLHelper.WriteWords(outputFilePath + ".xml", words);
        }
    }

    class Brackets
    {
        public bool IsBrackets { get; set; }
    }
}
