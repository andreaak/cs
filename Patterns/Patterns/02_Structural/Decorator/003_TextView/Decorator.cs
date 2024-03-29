﻿using System.Windows.Forms;

namespace Patterns._02_Structural.Decorator._003_TextView
{
    public abstract class Decorator : VisualComponent
    {
        protected VisualComponent component;

        protected Decorator(Form form, TextBox textBox)
            : base(form, textBox)
        {
        }

        public void SetComponent(VisualComponent visualComponent)
        {
            component = visualComponent;
        }

        public override void Draw()
        {
            if (component != null)
                component.Draw();
        }
    }
}
