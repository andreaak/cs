using System.Drawing;
using Patterns.Properties;

namespace Patterns._02_Structural.Proxy._002_ImageProxy.Pattern
{
    class PictureProxy : Graphic
    {
        Picture picture;

        public PictureProxy(string fileName)
        {
            this.fileName = fileName;
            PictureToShow = new Bitmap(Resources.startImg, 52, 62);
        }

        public override void Draw()
        {
            if (picture == null)
            {
                picture = new Picture(fileName);
            }
            picture.Draw();
        } 
        
        public override void Load()
        {
            PictureList.listPictures[PictureList.listPictures.IndexOf(this)] = this.picture;
        }
    }
}
