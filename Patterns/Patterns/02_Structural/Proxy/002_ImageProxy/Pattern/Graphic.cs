using System.Drawing;

namespace Patterns._02_Structural.Proxy._002_ImageProxy.Pattern
{
    abstract class Graphic
    {
        public string fileName;
        abstract public void Draw();
        abstract public void Load();

        public Image PictureToShow { get; set; }
    }
}
