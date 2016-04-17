using Patterns._03_Behavioral.State._005_StateMotivation.Context;

namespace Patterns._03_Behavioral.State._005_StateMotivation.State
{
    internal abstract class State
    {
        internal virtual void HandleMark(Father father, Mark mark)
        {
            ChangeState(father, mark);
        }

        protected abstract void ChangeState(Father father, Mark mark);
    }
}
