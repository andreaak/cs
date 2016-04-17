using System.Diagnostics;
using Patterns._03_Behavioral.State._005_StateMotivation.Context;

namespace Patterns._03_Behavioral.State._005_StateMotivation.State
{
    // Состояние сильной радости (S5)
    internal class StrongJoyState : State
    {
        internal StrongJoyState()
        {
            Debug.WriteLine("Отец в состоянии сильной радости:");
            Exult();
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

        // y5
        private void Exult()
        {
            Debug.WriteLine("Ликует и гордится сыном.");
        }
    }
}
