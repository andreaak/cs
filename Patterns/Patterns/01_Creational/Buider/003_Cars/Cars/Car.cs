using System.Text;

namespace Creational.Builder._003_Cars.Cars
{
    public class Car
    {
        public string Engine { get; set; }
        public string Frame { get; set; }
        public string Wheels { get; set; }
        public string Luxury { get; set; }
        public string Multimedia { get; set; }
        public string Safety { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Frame: {0}\n", Frame);
            sb.AppendFormat("Engine: {0}\n", Engine);
            sb.AppendFormat("Wheels: {0}\n", Wheels);
            sb.AppendFormat("Multimedia: {0}\n", Multimedia);
            sb.AppendFormat("Safety: {0}\n", Safety);
            sb.AppendFormat("Luxury: {0}\n", Luxury);

            return sb.ToString();
        }
    }
}