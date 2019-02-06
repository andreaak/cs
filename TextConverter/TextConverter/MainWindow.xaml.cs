using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using Utils;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DefaultSoundFormat = "mp3";

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

        public List<String> Languages { get; } = new List<string>() { Uk, Us };

        public string CurrentLanguage { get; set; }

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

        // Word

        public static DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Path
        {
            get
            {
                return GetValue(PathProperty) as string;
            }
            set
            {
                SetValue(PathProperty, value);
            }
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
                BindingExpression be = GetBindingExpression(PathProperty);
                if (be != null)
                    be.UpdateSource();
            }
        }

        private void ConvertFromCSVToXml_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxPath.Text) &&
                (Directory.Exists(textBoxPath.Text) || File.Exists(textBoxPath.Text));
        }

        private async void ConvertFromCSVToXml_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var data = new WordData()
            {
                Path = textBoxPath.Text,
                Region = CurrentLanguage,
                DownloadTranscription = checkBoxDownloadTranscription.IsChecked.GetValueOrDefault(),
                DownloadTranslation = checkBoxDownloadTranslation.IsChecked.GetValueOrDefault(),
                InfoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null),
                ErrorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null),
                Converter = new CSVtoXMLWordConverter()
            };

            await viewModelConverter.ConvertAsync(data);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        // Word

        private void buttonOpenWordXml_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
                {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
                };
            if (SelectFile.OpenFile("Open XML", "", ref path, extensions))
            {
                Text = path;
                BindingExpression be = GetBindingExpression(PathProperty);
                if (be != null)
                    be.UpdateSource();
            }
        }

        private void ConvertFromXmlToCSV_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxPath.Text) &&
                (Directory.Exists(textBoxPath.Text) || File.Exists(textBoxPath.Text));
        }

        private async void ConvertFromXmlToCSV_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var data = new WordData()
            {
                Path = textBoxPath.Text,
                Region = CurrentLanguage,
                DownloadTranscription = checkBoxDownloadTranscription.IsChecked.GetValueOrDefault(),
                DownloadTranslation = checkBoxDownloadTranslation.IsChecked.GetValueOrDefault(),
                InfoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null),
                ErrorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null),
                Converter = new XMLtoCSVWordConverter()
            };

            await viewModelConverter.ConvertAsync(data);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        // Verb

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

        private async void ConvertVerbToXML_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var data = new WordData()
            {
                Path = textBoxFileVerbCSV.Text,
                Region = CurrentLanguage,
                DownloadTranscription = checkBoxDownloadTranscription.IsChecked.GetValueOrDefault(),
                DownloadTranslation = checkBoxDownloadTranslation.IsChecked.GetValueOrDefault(),
                InfoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null),
                ErrorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null),
                Converter = new CSVtoXMLVerbConverter()
            };

            await viewModelConverter.RequestOutputAndConvertAsync(data);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        // Sounds

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
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.DownloadSoundsAsync(textBoxFileSounds.Text, DefaultSoundFormat, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void DownloadWordSounds_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.DownloadWordsSoundsAsync(textBoxFileSounds.Text, DefaultSoundFormat, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void DownloadVerbSounds_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.DownloadVerbsSoundsAsync(textBoxFileSounds.Text, DefaultSoundFormat, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        // Other 

        private async void CombineRuEnAndDownloadTranscriptions_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var data = new WordData()
            {
                Path = textBoxFileSounds.Text,
                Region = CurrentLanguage,
                DownloadTranscription = checkBoxDownloadTranscription.IsChecked.GetValueOrDefault(),
                DownloadTranslation = checkBoxDownloadTranslation.IsChecked.GetValueOrDefault(),
                InfoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null),
                ErrorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null),
                Converter = new TXTtoXMLWordConverter()
            };

            await viewModelConverter.RequestOutputAndConvertAsync(data);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void DownloadList_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var data = new WordData()
            {
                Path = textBoxFileSounds.Text,
                Region = CurrentLanguage,
                DownloadTranscription = checkBoxDownloadTranscription.IsChecked.GetValueOrDefault(),
                DownloadTranslation = checkBoxDownloadTranslation.IsChecked.GetValueOrDefault(),
                Separate = checkBoxSeparate.IsChecked.GetValueOrDefault(),
                InfoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null),
                ErrorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null),
                Converter = new TXTWithRanktoXMLWordConverter()
            };

            await viewModelConverter.RequestOutputAndConvertAsync(data);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }
    }
}
