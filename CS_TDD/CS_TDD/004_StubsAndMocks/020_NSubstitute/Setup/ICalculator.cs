namespace CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup
{
    public interface ICalculator
    {
        int Add(int a, int b);

        int Subtract(int a, int b);

        int Multiply(int a, int b);

        string Mode { get; set; }
    }
}
