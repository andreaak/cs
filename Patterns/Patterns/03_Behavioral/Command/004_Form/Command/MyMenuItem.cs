using System;
using System.Windows.Forms;

namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class MyMenuItem : ToolStripMenuItem
    {
        public Command MenuCommand { get; set; }

        public MyMenuItem(string text, Command command)
            : base(text)
        {
            MenuCommand = command;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (MenuCommand != null)
                MenuCommand.Execute();
        }
    }
}
