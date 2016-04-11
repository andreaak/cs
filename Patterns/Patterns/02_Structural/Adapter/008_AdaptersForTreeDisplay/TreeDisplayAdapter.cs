using System.Windows.Forms;

namespace Patterns.Structural.Adapter._008_AdaptersForTreeDisplay
{
    // TreeDisplay - (Target) : TreeView - (BaseTarget)
    abstract class TreeDisplay : TreeView
    {
        public abstract void Display(object tree);
    }
}
