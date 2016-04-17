using System.Diagnostics;
using Patterns._03_Behavioral.State._005_StateMotivation.Context;

namespace Patterns._03_Behavioral.State._005_StateMotivation.State
{
    // Состояние сильного гнева (S2)
    internal class StrongAngerState : State
    {
        internal StrongAngerState()
        {
            Debug.WriteLine("Отец в состоянии сильного гнева:");
            BeatBelt();
        }

        protected override void ChangeState(Father father, Mark mark)
        {
            switch (mark)
            {
                case Mark.Two:
                    {
                        father.State = new StrongAngerState(); // S2
                        break;
                    }
                case Mark.Five:
                    {
                        father.State = new NeutralState(); // S0
                        break;
                    }
            }
        }

        // y0
        private void BeatBelt()
        {
            Debug.WriteLine("Бьет сына ремнем.");
        }
    }
}
