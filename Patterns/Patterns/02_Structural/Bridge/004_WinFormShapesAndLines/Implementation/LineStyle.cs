using System.Drawing;

namespace Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Implementation
{
    // "Implementor"                                                                                         
    abstract class LineStyle
    {
        public abstract Pen Draw(Color color);
    }
}
