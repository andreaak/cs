using System.Xml.Serialization;

namespace Patterns._01_Creational.Singleton._001_Game
{
    [XmlRoot("LabirintProperties")] 
    public class Properties
    {
        private string mazeStyle;

        [XmlAttribute("MAZESTYLE")]
        public string MAZESTYLE
        {
            get { return mazeStyle; }
            set { mazeStyle = value; }
        }
    }
}
