using System.Drawing;

namespace Patterns.Structural.Composite._002_GraphicDesigner
{
    interface IGraphic
    {
        void Draw(Graphics device);
        void Add(IGraphic graphic);
        void Remove(IGraphic graphic);
        IGraphic GetChild(int child);
    }
}
