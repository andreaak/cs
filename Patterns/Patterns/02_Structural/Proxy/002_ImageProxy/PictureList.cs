using System.Collections.Generic;
using Patterns._02_Structural.Proxy._002_ImageProxy.Pattern;

namespace Patterns._02_Structural.Proxy._002_ImageProxy
{
    class PictureList
    {
       public static List<Graphic> listPictures { get; set; }

       static PictureList()
       {
           listPictures = new List<Graphic>();
       }
    }
}
