
namespace CS_TDD._004_StubsAndMocks._018_Moq.Setup
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

        void DoSomething();

        void DoSomething(string arg);

        string DoSomethingWithReturnString();

        bool DoSomethingWithReturnBool(string arg);

        string DoSomethingWithReturnString(string arg);

        bool DoSomethingWithReturnBool(int arg1, string arg2);

        string DoSomethingWithReturnString(int arg1, string arg2);

        bool TryParse(string arg, out string res);

        bool Submit(ref Employee instance);

        int GetCountThing();

        bool Add(int p);
    }
}
