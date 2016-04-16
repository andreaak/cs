namespace Patterns._01_Creational.Buider._004_House.Builder
{
    abstract class Builder
    {
        public abstract void BuildBasement();
        public abstract void BuildStorey();
        public abstract void BuildRoof();
        public abstract House.House GetResult();
    }
}
