using System.ComponentModel;

namespace CSTest._06_Interface
{
#if CS6
    class _20_INotifyPropertyChangedTests
    {
    }

    public class Person : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged.SafeInvoke(this, new PropertyChangedEventArgs(nameof(name)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public static class NullSafeExtensions
    {
        public static void SafeInvoke(this PropertyChangedEventHandler handler,
            object sender, PropertyChangedEventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
#endif
}
