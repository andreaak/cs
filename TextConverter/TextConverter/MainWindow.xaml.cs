using System.IO;
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

        private void ConvertToXML_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBoxDirectoryCSV.Text)
                && File.Exists(textBoxDirectoryCSV.Text);
        }

        private void ConvertToXML_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            string path = "";
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };

            if (SelectFile.SaveFile("Save XML", "", ref path, extensions))
            {
                var words = CSVReader.Read(textBoxDirectoryCSV.Text);
                XMLWriteHelper.Write(path, words);
            }

            System.Windows.MessageBox.Show("Done", "Operation Status");
        }

        private void buttonOpenCsv_Click(object sender, RoutedEventArgs e)
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
    }
}
