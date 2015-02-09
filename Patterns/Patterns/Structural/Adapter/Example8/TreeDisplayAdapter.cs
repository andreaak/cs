using System.Windows.Forms;

namespace Patterns.Structural.Adapter.Example8
{
    // TreeDisplay - (Target) : TreeView - (BaseTarget)
    abstract class TreeDisplay : TreeView
    {
        public abstract void Display(object tree);
    }
}
