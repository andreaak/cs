namespace Patterns._03_Behavioral.Iterator._002_Bank
{
    interface IEnumerator
    {
        bool MoveNext();
        void Reset();
        object Current { get; }
    }
}