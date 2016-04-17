using System.Diagnostics;
using Patterns._03_Behavioral.State._005_StateMotivation.Context;

namespace Patterns._03_Behavioral.State._005_StateMotivation.State
{
    // Состояние радости (S3)
    internal class JoyState : State
    {
        internal JoyState()
        {
            Debug.WriteLine("Отец в состоянии радости:");
            Joy();
        }

        protected override void ChangeState(Father father, Mark mark)
        {
            switch (mark)
            {
                case Mark.Two:
                    {
                        father.State = new PityState(); // S1
                        break;
                    }
                case Mark.Five:
                    {
                        father.State = new StrongJoyState(); // S5
                        break;
                    }
            }
        }

        // y4
        private void Joy()
        {
            Debug.WriteLine("Радуется успехам сына.");
        }
    }
}
