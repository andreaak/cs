using Patterns._03_Behavioral.State._005_StateMotivation.State;

namespace Patterns._03_Behavioral.State._005_StateMotivation.Context
{
    // Context
    public class Father
    {
        internal State.State State { get; set; }

        public Father()
        {
            State = new NeutralState();
        }

        public void FindOut(Mark mark)
        {
            State.HandleMark(this, mark);
        }
    }
}
