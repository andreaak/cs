namespace Patterns._02_Structural.Proxy._004_Surrogates
{
    class Surrogate : IHuman
    {
        IHuman @operator;

        public Surrogate(IHuman @operator)
        {
            this.@operator = @operator;
        }

        public void Request()
        {
            this.@operator.Request();
        }
    }
}
