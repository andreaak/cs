using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using TextConverter.Models;
using Utils;

namespace TextConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string OutputSounds = "Sounds/{1}/{0}_{1}.mp3";

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        private SynchronizationContext _uiSyncContext;

        public string Text
        {
            get
            {
                return GetValue(TextProperty) as string;
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string Description
        {
            get
            {
                return GetValue(DescriptionProperty) as string;
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        private ViewModelConvert viewModelConverter;
        public ViewModelConvert ViewModelConverter
        {
            get
            {
                return viewModelConverter;
            }
        }

        public MainWindow()
        {
            viewModelConverter = new ViewModelConvert();
            DataContext = this;
            InitializeComponent();
            _uiSyncContext = SynchronizationContext.Current;
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = Description;
                dlg.SelectedPath = Text;
                dlg.ShowNewFolderButton = true;
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Text = dlg.SelectedPath;
                    BindingExpression be = GetBindingExpression(TextProperty);
                    if (be != null)
                        be.UpdateSource();
                }
            }
        }

        private void Convert_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxDirectory.Text)
                && Directory.Exists(textBoxDirectory.Text)
                && comboSrcEncoding.SelectedValue != null
                && comboDstEncoding.SelectedValue != null;
        }

        private void Convert_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            viewModelConverter.Convert(textBoxDirectory.Text);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private void buttonOpenWordCsv_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
                {
                "CSV Files (*.csv)|*.csv|",
                "All Files (*.*)|*.*"
                };
            if (SelectFile.OpenFile("Open CSV", "", ref path, extensions))
            {
                Text = path;
                BindingExpression be = GetBindingExpression(TextProperty);
                if (be != null)
                    be.UpdateSource();
            }
        }

        private void ConvertWordToXML_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileWordCSV.Text)
                /*&& File.Exists(textBoxFileWordCSV.Text)*/;
        }

        private void ConvertWordToXML_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if(Directory.Exists(textBoxFileWordCSV.Text))
            {
                foreach (string file in Directory.EnumerateFiles(textBoxFileWordCSV.Text, "*.csv", SearchOption.TopDirectoryOnly))
                {
                    var words = CSVHelper.ReadWords(file);
                    XMLWriteHelper.WriteWords(textBoxFileWordCSV.Text+ Path.DirectorySeparatorChar + "lesson_" + Path.GetFileNameWithoutExtension(file) + ".xml", words);
                }
                return;
            }

            string path = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };

            if (SelectFile.SaveFile("Save XML", "", ref path, extensions))
            {
                var words = CSVHelper.ReadWords(textBoxFileWordCSV.Text);
                XMLWriteHelper.WriteWords(path, words);
            }

            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        public static DependencyProperty VerbTextProperty = DependencyProperty.Register("VerbText", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string VerbText
        {
            get
            {
                return GetValue(VerbTextProperty) as string;
            }
            set
            {
                SetValue(VerbTextProperty, value);
            }
        }

        private void buttonOpenVerbCsv_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
                {
                "CSV Files (*.csv)|*.csv|",
                "All Files (*.*)|*.*"
                };
            if (SelectFile.OpenFile("Open CSV", "", ref path, extensions))
            {
                VerbText = path;
                BindingExpression be = GetBindingExpression(VerbTextProperty);
                if (be != null)
                    be.UpdateSource();
            }
        }

        private void ConvertVerbToXML_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileVerbCSV.Text)
                && File.Exists(textBoxFileVerbCSV.Text);
        }

        private void ConvertVerbToXML_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };

            if (SelectFile.SaveFile("Save XML", "", ref path, extensions))
            {
                var words = CSVHelper.ReadVerbs(textBoxFileVerbCSV.Text);
                XMLWriteHelper.WriteVerbs(path, words);
            }

            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        public static DependencyProperty SoundsTextProperty = DependencyProperty.Register("SoundsText", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string SoundsText
        {
            get
            {
                return GetValue(SoundsTextProperty) as string;
            }
            set
            {
                SetValue(SoundsTextProperty, value);
            }
        }

        private void buttonOpenSoundsFile_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
                {
                "TXT Files (*.txt)|*.txt|",
                "All Files (*.*)|*.*"
                };
            if (SelectFile.OpenFile("Open Sounds List", "", ref path, extensions))
            {
                SoundsText = path;
                BindingExpression be = GetBindingExpression(SoundsTextProperty);
                if (be != null)
                    be.UpdateSource();
            }
        }

        private void DownloadSounds_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileSounds.Text)
                && File.Exists(textBoxFileSounds.Text);
        }

        private async void DownloadSounds_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var files = File.ReadAllLines(textBoxFileSounds.Text);
            await DownloadSoundsAsync(files);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private Task DownloadSoundsAsync(IList<string> files)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var web = new WebClient())
                {
                    for (int i = 0; i < files.Count; i += 2)
                    {
                        string normalized = files[i].Trim().ToLower();

                        try
                        {
                            _uiSyncContext.Post(state => labelInfo.Content = normalized, null);
                            if (!IsFileExist(normalized, CSVHelper.Us))
                            {
                                DownloadSound(web, normalized, CSVHelper.Us);
                            }
                            if (!IsFileExist(normalized, CSVHelper.Uk))
                            {
                                DownloadSound(web, normalized, CSVHelper.Uk);
                            }
                        }
                        catch (Exception ex)
                        {
                            _uiSyncContext.Post(state => labelError.Content = ex.Message, null);
                        }
                    }
                }
            });
        }

        private bool DownloadSound(WebClient web, string normalized, string lang)
        {
            string res = string.Format("http://wooordhunt.ru/data/sound/word/{0}/mp3/{1}.mp3",
                                lang, normalized);
            var bt = web.DownloadData(res);
            if (IsError(bt)) //html page
            {
                return false;
            }

            string dest = string.Format(OutputSounds, normalized, lang);
            File.WriteAllBytes(dest, bt);
            return true;
        }

        private bool IsFileExist(string normalized, string lang)
        {
            return File.Exists(string.Format(OutputSounds, normalized, lang));
        }

        private bool IsError(byte[] bt)
        {
            return bt.Length < 4 ||
                   (bt[0] == 60 && bt[1] == 33 && bt[2] == 68 && bt[3] == 79);//html page
        }

        private void DownloadTranscription_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileSounds.Text)
                && File.Exists(textBoxFileSounds.Text);
        }

        private void DownloadTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var files = File.ReadAllLines(textBoxFileSounds.Text);
            var words = DownloadTranscriptions(files);
            XMLWriteHelper.WriteWords("files.xml", words);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private List<WordItem> DownloadTranscriptions(IList<string> files)
        {
            List<WordItem> words = new List<WordItem>();
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                for (int i = 0; i < files.Count - 1;)
                {
                    string en = files[i++];
                    string ru = files[i++];
                    string normalized = en.Trim().ToLower();

                    string tr = DownloadTranscription(web, normalized);

                    if (string.IsNullOrEmpty(tr))
                    {
                        continue;
                    }
                    var item = new WordItem();
                    item.AddItem(CSVHelper.Ru, ru);
                    item.AddItem(CSVHelper.En, en);
                    item.AddItem(CSVHelper.EnTr, tr);
                    words.Add(item);
                }
            }
            return words;
        }

        private void DownloadVerbTranscription_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileSounds.Text)
                && File.Exists(textBoxFileSounds.Text);
        }

        private void DownloadVerbTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var verbs = CSVHelper.ReadVerbs(textBoxFileSounds.Text);
            DownloadTranscriptions(verbs);
            XMLWriteHelper.WriteVerbs("irregular.xml", verbs);
            CSVHelper.WriteVerbs("irregular.csv", verbs);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private void DownloadTranscriptions(IList<EnVerbItem> verbs)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var verb in verbs)
                {
                    if (string.IsNullOrEmpty(verb.InfinitiveTranscription))
                    {
                        var temps = GetWords(verb.Infinitive);
                        verb.InfinitiveTranscription = DownloadTranscription(web, temps);
                    }
                    if (string.IsNullOrEmpty(verb.PastSimpleTranscription))
                    {
                        var temps = GetWords(verb.PastSimple);
                        verb.PastSimpleTranscription = DownloadTranscription(web, temps);
                    }
                    if (string.IsNullOrEmpty(verb.PastPaticipleTranscription))
                    {
                        var temps = GetWords(verb.PastPaticiple);
                        verb.PastPaticipleTranscription = DownloadTranscription(web, temps);
                    }
                }
            }
        }

        private string[] GetWords(string item)
        {
            return item.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string DownloadTranscription(WebClient web, string[] items)
        {
            StringBuilder res = new StringBuilder();
            foreach (var item in items)
            {
                string normalized = item.Trim().ToLower();
                string tr = DownloadTranscription(web, normalized);
                if (res.Length != 0)
                {
                    res.Append("/ ");
                }
                res.Append(tr);
            }
            return res.ToString();
        }

        private string DownloadTranscription(WebClient web, string normalized)
        {
            string tr = null;
            try
            {
                string res = string.Format("http://wooordhunt.ru/word/{0}", normalized);
                var html = web.DownloadString(res);
                tr = ParseTranscription(html, normalized, CSVHelper.Uk);
            }
            catch (Exception ex)
            {
                labelError.Content = ex.Message;
            }
            return tr;
        }

        private string ParseTranscription(string html, string normalized, string lang)
        {
            const string usPattern = "<span title=\"американская транскрипция слова {0}\" class=\"transcription\">";
            const string ukPattern = "<span title=\"британская транскрипция слова {0}\" class=\"transcription\">";
            string startPattern = lang == "us" ? usPattern : ukPattern;
            string search = string.Format(startPattern, normalized);
            int startIndex = html.IndexOf(search, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return string.Empty;
            }
            startIndex += search.Length;

            string endPattern = "</span>";
            int endIndex = html.IndexOf(endPattern, startIndex, StringComparison.OrdinalIgnoreCase);
            string output = "[" + html.Substring(startIndex, endIndex - startIndex)
                .Replace("|", "").Trim() + "]";
            return output;
        }
    }
}
