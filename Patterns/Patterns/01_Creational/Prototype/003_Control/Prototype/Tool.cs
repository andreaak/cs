
using System.Windows.Forms;

namespace Patterns._01_Creational.Prototype._003_Control.Prototype
{
    /// <summary>
    /// Абстрактный класс Tool для определения инструментов в палитре
    /// </summary>
    public abstract class Tool
    {
        protected Form form = null;
        protected Panel scoreSheetPanel;

        protected Tool(Panel panel)
        {
            this.scoreSheetPanel = panel;
        }

        public abstract void Manipulate();
    }
}
