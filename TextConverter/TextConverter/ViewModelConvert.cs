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
        private const string OutputWordsSounds = "Sounds/{1}_{3}/{2}_{1}/{0}_{1}.{3}";
        private const string OutputVerbsSounds = "Sounds/Irregular/{1}_{3}/{2}_{1}/{0}_{1}.{3}";
        //private const string SoundDestination = "http://wooordhunt.ru/data/sound/word/{0}/mp3/{1}.mp3";
        private const string SoundDestination = "http://wooordhunt.ru/data/sound/word/{0}/{2}/{1}.{2}";

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

        public async Task ConvertWordToXmlAsync(string path, Action<string> infoAction, Action<string> errorAction)
        {
            if (Directory.Exists(path))
            {
                await ConvertWordsFromCSVToXMLAsync(path, null, false, infoAction, errorAction);
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
                    //await DownloadWordsTranscriptionAsync(words, null, infoAction, errorAction);
                    XMLWriteHelper.WriteWords(outputFilePath, words);
                }
            }
        }

        public async Task ConvertWordToXmlAndDownloadTranscriptionAsync(string path, string region, 
            Action<string> infoAction, Action<string> errorAction)
        {
            if (Directory.Exists(path))
            {
                await ConvertWordsFromCSVToXMLAsync(path, region, true, infoAction, errorAction);
            }
            else
            {
                await ConvertWordFromCSVToXMLAsync(path, region, true, infoAction, errorAction);
            }
        }

        private async Task DownloadWordsTranscriptionForceAsync(IList<WordItem> words, string region, 
            Action<string> infoAction, Action<string> errorAction)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var word in words)
                {
                    string normalized = GetNormalized(word.GetItem(CSVHelper.En));
                    infoAction?.Invoke(normalized);
                    string trExisted = GetNormalized(word.GetItem(CSVHelper.EnTr));

                    string tr;
                    if (IsMultiWord(normalized))
                    {
                        tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, region, errorAction);
                    }
                    else
                    {
                        tr = await DownloadTranscriptionAsync(web, normalized, ParseTranscription, errorAction, region);
                    }
                    if (!string.IsNullOrEmpty(tr) && trExisted != tr)
                    {
                        word.AddItem(CSVHelper.EnTr, tr);
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
                    string normalized = GetNormalized(word.GetItem(CSVHelper.En));
                    infoAction?.Invoke(normalized);
                    string trExisted = GetNormalized(word.GetItem(CSVHelper.EnTr));
                    if (!string.IsNullOrEmpty(trExisted))
                    {
                        continue;
                    }
                    string tr;
                    if (IsMultiWord(normalized))
                    {
                        tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, region, errorAction);
                    }
                    else
                    {
                        tr = await DownloadTranscriptionAsync(web, normalized, ParseTranscription, errorAction, region);
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

        private async Task ConvertWordsFromCSVToXMLAsync(string directory, string region, bool isLoadTranscription, 
            Action<string> infoAction, Action<string> errorAction)
        {
            foreach (string filePath in Directory.EnumerateFiles(directory, "*.csv", SearchOption.TopDirectoryOnly))
            {
                await ConvertWordFromCSVToXMLAsync(filePath, region, isLoadTranscription, infoAction, errorAction);
            }
            return;
        }

        private async Task ConvertWordFromCSVToXMLAsync(string filePath, string region, bool isLoadTranscription, Action<string> infoAction, Action<string> errorAction)
        {
            var words = CSVHelper.ReadWords(filePath);
            if(isLoadTranscription)
            {
                await DownloadWordsTranscriptionForceAsync(words, region, infoAction, errorAction);
            }
            string suffix = string.IsNullOrEmpty(region) ? "" : $"_{region}";
            string outputFilePath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar
                + "lesson_" + Path.GetFileNameWithoutExtension(filePath) + suffix + ".xml";
            XMLWriteHelper.WriteWords(outputFilePath, words);
            CSVHelper.WriteWords(outputFilePath + ".csv", words);
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
                XMLWriteHelper.WriteVerbs(outputFilePath, verbs);
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

        private string GetNormalized(string word)
        {
            return word.Trim().ToLower();
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
                        item.AddItem(CSVHelper.Ru, ru);
                        item.AddItem(CSVHelper.En, en);
                        if (!string.IsNullOrEmpty(tr))
                        {
                            item.AddItem(CSVHelper.EnTr, tr);
                        }
                        wordItems.Add(item);
                    }
                }

                XMLWriteHelper.WriteWords(outputFilePath, wordItems);
            }
        }

        // Other

        private async Task<string> DownloadTranscriptionAsync(WebClient web, string normalized,
            Func<string, string, string, string> parseAction, Action<string> errorAction, string region)
        {
            string tr = null;
            try
            {
                string res = string.Format("http://wooordhunt.ru/word/{0}", normalized);
                var html = await web.DownloadStringTaskAsync(res);
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
                            if(notFinded.Contains(normalized))
                            {
                                continue;
                            }
                            bool res = DownloadSounds(web, normalized, OutputWordsSounds, soundFormat, infoAction, errorAction);
                            if(!res)
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
                        string normalized = GetNormalized(word.GetItem(CSVHelper.En));

                        DownloadSounds(web, normalized, OutputWordsSounds, soundFormat, infoAction, errorAction);
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
                        DownloadVerbSounds(web, verb.Infinitive, soundFormat, infoAction, errorAction);
                        DownloadVerbSounds(web, verb.PastSimple, soundFormat, infoAction, errorAction);
                        DownloadVerbSounds(web, verb.PastPaticiple, soundFormat, infoAction, errorAction);
                    }
                }
            });
        }

        private void DownloadVerbSounds(WebClient web, string verbsCombined, string soundFormat, Action<string> infoAction, Action<string> errorAction)
        {
            var verbs = verbsCombined.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var verb in verbs)
            {
                DownloadSounds(web, GetNormalized(verb), OutputVerbsSounds, soundFormat, infoAction, errorAction);
            }
        }

        private bool DownloadSounds(WebClient web, string normalized, string destination, string soundFormat, 
            Action<string> infoAction, Action<string> errorAction)
        {
            bool res = true;
            try
            {
                infoAction(normalized);
                if (!IsWordFileExist(normalized, CSVHelper.Us, destination, soundFormat))
                {
                    res = DownloadSound(web, normalized, CSVHelper.Us, destination, soundFormat);
                }
                if (!IsWordFileExist(normalized, CSVHelper.Uk, destination, soundFormat))
                {
                    res = DownloadSound(web, normalized, CSVHelper.Uk, destination, soundFormat);
                }
            }
            catch (Exception ex)
            {
                errorAction(ex.Message);
                res = false;
            }
            return res;
        }

        private bool DownloadSound(WebClient web, string normalized, string region, 
            string destination, string soundFormat)
        {
            string res = string.Format(SoundDestination, region, normalized, soundFormat);
            var bt = web.DownloadData(res);
            if (IsError(bt)) //html page
            {
                return false;
            }

            string dest = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + 
                        string.Format(destination, normalized, region, normalized[0], soundFormat);
            string folder = Path.GetDirectoryName(dest);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllBytes(dest, bt);
            return true;
        }

        private bool IsWordFileExist(string normalized, string region, string destination, string soundFormat)
        {
            return File.Exists(string.Format(destination, normalized, region, normalized[0], soundFormat));
        }

        private bool IsError(byte[] bt)
        {
            return bt.Length < 4 ||
                   (bt[0] == 60 && bt[1] == 33 && bt[2] == 68 && bt[3] == 79);//html page
        }
    }

    class Brackets
    {
        public bool IsBrackets { get; set; }
    }
}
