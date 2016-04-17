namespace Patterns._03_Behavioral.Iterator._001_Base
{
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }
}