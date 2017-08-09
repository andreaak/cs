using System;

namespace ASPWebFormsTest._21_UserControls
{
    public class _07_EventArgs : EventArgs
    {
        public string ErrorMessage { get; set; }

        public _07_EventArgs(string errorMessge)
        {
            this.ErrorMessage = errorMessge;
        }
    }
}