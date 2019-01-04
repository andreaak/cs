using System;
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
        private const string DefaultLanguage = Uk;
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

        private void ConvertFromCSVToXml_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileWordCSV.Text)
                /*&& File.Exists(textBoxFileWordCSV.Text)*/;
        }

        private async void ConvertFromCSVToXml_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.ConvertFromCSVToXmlAsync(textBoxFileWordCSV.Text, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void ConvertFromCSVToXmlAndDownloadUkWordTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await ConvertFromCSVToXmlAndDownloadTranscription(Uk);
        }

        private async void ConvertFromCSVToXmlAndDownloadUsWordTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await ConvertFromCSVToXmlAndDownloadTranscription(Us);
        }

        private async Task ConvertFromCSVToXmlAndDownloadTranscription(string region)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.ConvertFromCSVToXmlAndDownloadTranscriptionAsync(textBoxFileWordCSV.Text, region, infoAction, errorAction);
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
                XMLHelper.WriteVerbs(path, words);
            }

            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void ConvertVerbToXMLAndDownloadUkVerbTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await ConvertVerbFromCSVToXmlAndDownloadTranscription(Us);
        }

        private async void ConvertVerbToXMLAndDownloadUsVerbTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await ConvertVerbFromCSVToXmlAndDownloadTranscription(Us);
        }

        private async Task ConvertVerbFromCSVToXmlAndDownloadTranscription(string region)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.ConvertVerbToXMLAndDownloadVerbTranscriptionAsync(textBoxFileVerbCSV.Text, region, infoAction, errorAction);
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

        private async void DownloadPlainListTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            var files = File.ReadAllLines(textBoxFileSounds.Text);
            await viewModelConverter.CombineRuEnAndDownloadPlainListTranscriptionsAsync(textBoxFileSounds.Text, DefaultLanguage,infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }


        // Word

        private void buttonOpenWordXml_Click(object sender, RoutedEventArgs e)
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

        private void ConvertWordToCSV_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxFileWordXml.Text)
                /*&& File.Exists(textBoxFileWordCSV.Text)*/;
        }

        private async void ConvertWordToCSV_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.ConvertFromXMLToCSVAsync(textBoxFileWordXml.Text, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void ConvertWordToCSVAndDownloadUkWordTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.ConvertFromXMLToCsvAndDownloadTranscriptionAsync(textBoxFileWordXml.Text, Uk, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void ConvertWordToCSVAndDownloadUsWordTranscription_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.ConvertFromXMLToCsvAndDownloadTranscriptionAsync(textBoxFileWordXml.Text, Us, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private async void DownloadPopularityList_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Action<string> infoAction = (info) => _uiSyncContext.Post(state => labelInfo.Content = info, null);
            Action<string> errorAction = (error) => _uiSyncContext.Post(state => labelError.Content = error, null);

            await viewModelConverter.DownloadPopularityListAsync(textBoxFileSounds.Text, Uk, infoAction, errorAction);
            System.Windows.MessageBox.Show("Done", "Operation Status");

        }
    }
}
