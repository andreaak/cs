using System.Drawing;

namespace Patterns.Structural.Bridge.Example4
{
    // "Implementor"                                                                                         
    abstract class LineStyle
    {
        public abstract Pen Draw(Color color);
    }
}
