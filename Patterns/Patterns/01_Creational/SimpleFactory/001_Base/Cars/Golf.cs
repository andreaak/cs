
namespace Patterns._01_Creational.SimpleFactory._001_Base.Cars
{
    public class Golf : Car
    {

        public Golf()
        {
            Name = "Golf";
            Body = "Hatchback";
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
