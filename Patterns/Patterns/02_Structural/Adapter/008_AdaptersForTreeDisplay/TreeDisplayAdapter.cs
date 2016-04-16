using System.Windows.Forms;

namespace Patterns._02_Structural.Adapter._008_AdaptersForTreeDisplay
{
    // TreeDisplay - (Target) : TreeView - (BaseTarget)
    abstract class TreeDisplay : TreeView
    {
        public abstract void Display(object tree);
    }
}
