
namespace Patterns._03_Behavioral.Iterator._004_Enumerator
{
    interface IEnumerator
    {
        bool MoveNext();
        void Reset();
        object Current { get; }
    }
}