using System.Diagnostics;
using Patterns._03_Behavioral.State._005_StateMotivation.Context;

namespace Patterns._03_Behavioral.State._005_StateMotivation.State
{
    // Состояние жалости (S1)
    internal class PityState : State
    {
        internal PityState()
        {
            Debug.WriteLine("Отец в состоянии жалости:");
            Calm();
        }

        protected override void ChangeState(Father father, Mark mark)
        {
            switch (mark)
            {
                case Mark.Two:
                    {
                        father.State = new AngerState(); // S4
                        break;
                    }
                case Mark.Five:
                    {
                        father.State = new NeutralState(); // S0
                        break;
                    }
            }
        }

        // y2
        private void Calm()
        {
            Debug.WriteLine("Успокаивает сына.");
        }
    }
}
