﻿using System.Drawing;
using System.Windows.Forms;

namespace Patterns.Structural.Adapter.Example4
{
    /// <summary>
    /// Класс TextView - адаптируемый.
    /// определяет существующий интерфейс, который нуждается в адаптации
    /// </summary>

    class TextView
    {
        Label label;

        public TextView()
        {
            label = new Label();
        }

        public Label GetExtend()
        {
            label.ForeColor = Color.DarkMagenta;
            label.Font = new Font(label.Font, label.Font.Style | FontStyle.Bold);
            label.BorderStyle = BorderStyle.None;
            label.TextAlign = ContentAlignment.TopLeft;
            label.FlatStyle = FlatStyle.Standard;
            label.AutoSize = true;

            return label;
        }
    }
}