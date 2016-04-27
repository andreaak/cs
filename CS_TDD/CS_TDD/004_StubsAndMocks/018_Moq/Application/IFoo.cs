
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
        
        bool DoSomething(string arg);

        string DoSomething2(string arg);

        string DoSomething();

        bool DoSomething(int arg1, string arg2);

        bool TryParse(string arg, out string res);

        bool Submit(ref Bar instance);

        int GetCountThing();

        bool Add(int p);
    }
}
