using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using Utils;

namespace TextConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

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
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            DownloadFiles(new string[] { "trust", "hello", "trrr"});
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
                && File.Exists(textBoxFileWordCSV.Text);
        }

        private void ConvertWordToXML_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };

            if (SelectFile.SaveFile("Save XML", "", ref path, extensions))
            {
                var words = CSVReader.ReadWords(textBoxFileWordCSV.Text);
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
                var words = CSVReader.ReadVerbs(textBoxFileVerbCSV.Text);
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

        private void DownloadSounds_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var files = File.ReadAllLines(textBoxFileSounds.Text);
            DownloadFiles(files);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private void DownloadFiles(IList<string> files)
        {
            using (var web = new WebClient())
            {
                foreach (var file in files)
                {
                    string normalized = file.Trim().ToLower();
                    string res = string.Format("http://wooordhunt.ru/data/sound/word/us/mp3/{0}.mp3", normalized);
                    var bt = web.DownloadData(res);
                    if (bt.Length < 4 ||
                        (bt[0] == 60 && bt[1] == 33 && bt[2] == 68 && bt[3] == 79)) //html page
                    {
                        continue;
                    }
                    string output = "Sounds/" + normalized + ".mp3";
                    if(!File.Exists(output))
                    {
                        File.WriteAllBytes(output, bt);
                    }
                }
            }
        }
    }
}
