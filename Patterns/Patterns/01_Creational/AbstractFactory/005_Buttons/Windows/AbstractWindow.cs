﻿using System.Drawing;
using System.Windows.Forms;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Windows
{
    // AbstractProductB
    abstract class AbstractWindow : Form
    {
        // void Interact(AbstractBroductA apa)
        public abstract void AddToCollection(AbstractButton button);

        public AbstractWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 172);
        }
    }
}
