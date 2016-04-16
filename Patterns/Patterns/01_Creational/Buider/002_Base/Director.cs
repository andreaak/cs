namespace Patterns._01_Creational.Buider._002_Base
{
    class Director
    {
        Builder.Builder builder;

        public Director(Builder.Builder builder)
        {
            this.builder = builder;
        }

        public void Construct()
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartC();
        }
    }
}
