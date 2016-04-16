namespace Patterns._02_Structural.Adapter._002_Class
{
    class Adapter : Adaptee, ITarget
    {
        public void Request()
        {
            base.SpecificRequest();
        }
    }
}
