using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.CommandWpf;

namespace CS_TDD._005_xUnit._02_Asserting.Setup
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _text;

        public MainViewModel()
        {
            Text = "Enter some text";
            ConvertToUpperCommand = new RelayCommand(CovertText);
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                // BUG for demo purposes
                OnPropertyChanged();
            }
        }

        public RelayCommand ConvertToUpperCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void CovertText()
        {
            Text = Text.ToUpperInvariant();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}