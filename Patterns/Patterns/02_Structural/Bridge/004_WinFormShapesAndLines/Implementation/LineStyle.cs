using System.Drawing;

namespace Patterns.Structural.Bridge._004_WinFormShapesAndLines
{
    // "Implementor"                                                                                         
    abstract class LineStyle
    {
        public abstract Pen Draw(Color color);
    }
}
