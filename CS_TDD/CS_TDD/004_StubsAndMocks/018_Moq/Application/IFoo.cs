
namespace CS_TDD._004_StubsAndMocks._018_Moq.Application
{
    public interface IFoo
    {
        string Name
        {
            get;
            set;
        }

        int Value
        {
            get;
            set;
        }

        bool DoSomethingWithReturn(string arg);

        string DoSomethingWithReturnString(string arg);

        void DoSomethingWithoutReturn(string arg);

        string DoSomething();

        bool DoSomething(int arg1, string arg2);

        bool TryParse(string arg, out string res);

        bool Submit(ref Employee instance);

        int GetCountThing();

        bool Add(int p);
    }
}
