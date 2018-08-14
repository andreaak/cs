using System;
using System.Windows.Forms;

namespace Note
{
    public interface IMainView
    {
        Form Form { get; }

        void Refresh(bool res);

        void OnChanged(); 
    }
}
