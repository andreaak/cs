﻿using System.Drawing;
using System.Windows.Forms;
using Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Abstraction;
using Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Implementation;

namespace Patterns._02_Structural.Bridge._004_WinFormShapesAndLines
{
    public class Form1 : Form
    {
        public Form1()
        {
            this.Text = "Bridge";
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Отрисовка фигур.
            //Figure.Draw(this, new Square(), new DotLine(), Color.Gray);
            //Figure.Draw(this, new Triangle(), new DotLine(), Color.Red);
            //Figure.Draw(this, new Pentagon(), new DotLine(), Color.Blue);
            Figure.Draw(this, new Triangle(), new DashDotLine(), Color.Turquoise);
            //Figure.Draw(this, new Square(), new DotLine(), Color.Magenta);
        }
    }
}
