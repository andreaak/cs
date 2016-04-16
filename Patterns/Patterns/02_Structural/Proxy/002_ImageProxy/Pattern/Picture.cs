using System;
using System.Drawing;

namespace Patterns._02_Structural.Proxy._002_ImageProxy.Pattern
{
    class Picture : Graphic
    {
        public Picture(string fileName)
        {
            this.fileName = fileName;
        }

        public override void Draw()
        {
            PictureToShow = Image.FromFile(fileName);
        }

        public override void Load()
        {
            throw new InvalidOperationException();
        }
    }
}
