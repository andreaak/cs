
namespace Patterns.Creational.SimpleFactory._001_Base.Cars
{
    public class Touareg : Car
    {

        public Touareg()
        {
            Name = "Touareg";
            Body = "Big Crossover";
        }

        //public override void Configure()
        //{
        //    Debug.WriteLine("Configuring {0}", Name);
        //    Debug.WriteLine("Body is: {0}", Body);

        //    //Engine = _factory.CreateEngine();
        //    //PaintColor = _factory.CreatePaint();
        //    //Wheels = _factory.CreateWheels();
        //}
    }
}
